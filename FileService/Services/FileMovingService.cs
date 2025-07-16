using FileService.Contracts.IServices;
using FileService.Enums;

namespace FileService.Services;

public class FileMovingService : IFileMovingService
{
    public void Move(FileSystemEventArgs e, DestinationType destinationType)
    {
        var destination = destinationType == DestinationType.Succeeded
            ? @$"{Constants.Directories.Succeeded}\{e.Name}"
            : @$"{Constants.Directories.Failed}\{e.Name}";
        
        File.Move(e.FullPath, destination);
    }
}