using FileService.Contracts.IServices;
using Shared.Enums;
using Shared.Producer;
using Shared.Requests;

namespace FileService.Services;

public class FileWatchingService(
    IFileProcessingService fileProcessingService,
    IProducer producer) : IFileWatchingService
{
    private string ServiceName => GetType().Name;
    
    private async Task OnCreatedAsync(object _, FileSystemEventArgs e)
    {
        try
        {
            await fileProcessingService.ProcessAsync(e);
        }
        catch (Exception ex)
        {
            await producer.PublishToLoggingService(new LoggingRequest
            {
                Message = $"[{ServiceName}]: An exception occurred - {ex.Message}",
                LogType = LogType.Error,
            });
        }
    }
    
    public async Task Watch(CancellationToken cancellationToken)
    {
        var fileSystemWatcher = new FileSystemWatcher
        {
            Path = Constants.Directories.Processing,
            Filter = "*.txt",
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };
        
        fileSystemWatcher.Created += (s, e) => _ = OnCreatedAsync(s, e);

        await producer.PublishToLoggingService(new LoggingRequest
        {
            Message = $"[{ServiceName}]: Watching...",
            LogType = LogType.Information
        });
        
        await Task.Delay(-1, cancellationToken);
    }
}