using FileService.Contracts.IServices;
using FileService.Enums;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace FileService.Services;

public class FileMovingService(IProducer producer) : IFileMovingService
{
    public void Move(FileSystemEventArgs e, DestinationType destinationType)
    {
        var destination = destinationType == DestinationType.Succeeded
            ? @$"{Constants.Directories.Succeeded}\{e.Name}"
            : @$"{Constants.Directories.Failed}\{e.Name}";

        producer.PublishToLoggingService(new LoggingRequest()
        {
            Message = $"Moving {e.Name} to {destination}",
            LogType = LogType.Information
        });
        
        File.Move(e.FullPath, destination);
    }
}