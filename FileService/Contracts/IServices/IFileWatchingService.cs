namespace FileService.Contracts.IServices;

public interface IFileWatchingService
{
    Task Watch(CancellationToken cancellationToken);
}