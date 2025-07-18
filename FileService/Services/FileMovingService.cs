using FileService.Contracts.IServices;
using FileService.Enums;
using Shared;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace FileService.Services;

public class FileMovingService(IProducer producer) : IFileMovingService
{
    private string ServiceName => GetType().Name;
    
    public async Task Move(FileSystemEventArgs e, DestinationType destinationType)
    {
        var destination = destinationType == DestinationType.Succeeded
            ? @$"{Constants.Directories.Succeeded}\{e.Name}"
            : @$"{Constants.Directories.Failed}\{e.Name}";
        
        await producer.PublishAsync(
            Queues.Logging,
            new LoggingRequest
            {
                Message = $"[{ServiceName}]: Moving '{e.Name}' to '{destination}'",
                LogType = LogType.Information
            });
        
        File.Move(e.FullPath, destination);
    }
}