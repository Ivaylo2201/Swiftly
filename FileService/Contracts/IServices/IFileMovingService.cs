using FileService.Enums;

namespace FileService.Contracts.IServices;

public interface IFileMovingService
{
    void Move(FileSystemEventArgs e, DestinationType destinationType);
}