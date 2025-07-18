using FileService.Enums;

namespace FileService.Contracts.IServices;

public interface IFileMovingService
{
    Task Move(FileSystemEventArgs e, DestinationType destinationType);
}