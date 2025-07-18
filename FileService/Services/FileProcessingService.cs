using System.Text;
using System.Text.Json;
using FileService.Contracts.IServices;
using FileService.Enums;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using Shared.Enums;
using Shared.Requests;

namespace FileService.Services;

public class FileProcessingService(
    ISwiftFileEntityRepository swiftFileRepository,
    IFileMovingService fileMovingService) : IFileProcessingService
{
    private string ServiceName => GetType().Name;
    
    public async Task ProcessAsync(FileSystemEventArgs e)
    {
        var content = await File.ReadAllTextAsync(e.FullPath);
        
        var messages = Constants.Patterns.FileParseRegex()
            .Split(content)
            .Select((message, index) => (Message: message.Replace("\x01", "").Replace("\x03", ""), Index: index + 1))
            .ToList();
        
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: Queues.MessageReply, exclusive: false);
        var consumer = new AsyncEventingBasicConsumer(channel);
        var tasks = new Dictionary<string, TaskCompletionSource<bool>>();
        var allTasks = new List<Task<bool>>();
        
        consumer.ReceivedAsync += (_, ea) =>
        {
            var correlationId = ea.BasicProperties.CorrelationId!;

            if (!tasks.TryGetValue(correlationId, out var tcs))
                return Task.CompletedTask;

            tcs.SetResult(bool.Parse(Encoding.UTF8.GetString(ea.Body.ToArray())));
            tasks.Remove(correlationId);

            return Task.CompletedTask;
        };
            
        await channel.BasicConsumeAsync(Queues.MessageReply, autoAck: true, consumer: consumer);

        foreach (var tuple in messages)
        {
            var loggingBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new LoggingRequest
            {
                Message = $"[{ServiceName}]: Processing message #{tuple.Index}",
                LogType = LogType.Information
            }));
            
            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: Queues.Logging,
                mandatory: false,           
                body: loggingBody);
            
            // Message sending
            var correlationId = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<bool>();
            tasks[correlationId] = tcs;
            allTasks.Add(tcs.Task);
            
            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new MessageRequest
            {
                Message = tuple.Message,
                Index = tuple.Index
            }));

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: Queues.Message,
                mandatory: false,
                basicProperties: new BasicProperties { ReplyTo = Queues.MessageReply, CorrelationId = correlationId },
                body: messageBody);
        }
        
        var isSuccess = (await Task.WhenAll(allTasks)).All(t => t);
        
        Console.WriteLine(isSuccess);
        
        await swiftFileRepository.CreateAsync(new SwiftFileEntity
        {
            SwiftFileName = e.Name ?? e.FullPath,
            SwiftMessageCount = messages.Count,
            IsSuccess = isSuccess
        });
        
        fileMovingService.Move(e, isSuccess ? DestinationType.Succeeded : DestinationType.Failed);
        
        await channel.CloseAsync();
        await connection.CloseAsync();
    }
}