namespace FileService.Contracts.IServices;

public interface IFileProcessingService
{
    Task ProcessAsync(FileSystemEventArgs e);
}