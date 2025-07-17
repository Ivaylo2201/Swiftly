using FileService.Contracts.IServices;
using FileService.Enums;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Entities;
using Shared;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace FileService.Services;

public class FileProcessingService(
    ISwiftFileEntityRepository swiftFileRepository,
    IFileMovingService fileMovingService,
    IProducer producer) : IFileProcessingService
{
    private string ServiceName => GetType().Name;
    
    public async Task ProcessAsync(FileSystemEventArgs e)
    {
        var content = await File.ReadAllTextAsync(e.FullPath);
        
        var messages = Constants.Patterns.FileParseRegex()
            .Split(content)
            .Select((message, index) => (Message: message.Replace("\x01", "").Replace("\x03", ""), Index: index + 1))
            .ToList();

        foreach (var tuple in messages)
        {
            await producer.PublishToLoggingService(new LoggingRequest
            {
                Message = $"[{ServiceName}]: Processing message #{tuple.Index}",
                LogType = LogType.Information,
            });

            await producer.PublishToMessageService(new MessageRequest { Message = tuple.Message, Index = tuple.Index });
        }

        // await swiftFileRepository.CreateAsync(new SwiftFileEntity
        // {
        //     SwiftFileName = e.Name ?? e.FullPath,
        //     SwiftMessageCount = messages.Count,
        //     IsSuccess = isSuccess
        // });
        //
        // fileMovingService.Move(e, isSuccess ? DestinationType.Succeeded : DestinationType.Failed);
    }
}