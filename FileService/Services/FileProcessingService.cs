using FileService.Contracts.IServices;
using FileService.Enums;
using MessageService.Contracts.IServices;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using Shared;
using Shared.Enums;
using Shared.Requests;

namespace FileService.Services;

public class FileProcessingService(
    IMessageProcessingService messageProcessingService,
    ISwiftFileEntityRepository swiftFileRepository,
    IFileMovingService fileMovingService,
    IProducer producer) : IFileProcessingService
{
    public async Task ProcessAsync(FileSystemEventArgs e)
    {
        var isSuccess = true;
        var content = await File.ReadAllTextAsync(e.FullPath);
        
        var messages = Constants.Patterns.FileParseRegex()
            .Split(content)
            .Select((message, index) => (Message: message.Replace("\x01", "").Replace("\x03", ""), Index: index))
            .ToList();

        foreach (var tuple in messages)
        {
            await producer.PublishMessageToLoggingService(new LoggingRequest
            {
                Message = $"[FileProcessingService]: Processing message #{tuple.Index}",
                LogType = LogType.Information,
            });
            
            var swiftMessage = await messageProcessingService.Process(tuple.Message);
            
            if (!swiftMessage.IsSuccess)
                isSuccess = false;
        }

        await swiftFileRepository.CreateAsync(new SwiftFileEntity
        {
            SwiftFileName = e.Name ?? e.FullPath,
            SwiftMessageCount = messages.Count,
            IsSuccess = isSuccess
        });

        fileMovingService.Move(e, isSuccess ? DestinationType.Succeeded : DestinationType.Failed);
    }
}