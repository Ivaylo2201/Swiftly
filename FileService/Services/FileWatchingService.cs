using FileService.Contracts.IServices;
using LoggingService.Contracts.IServices;
using Shared.Enums;

namespace FileService.Services;

public class FileWatchingService(
    IFileProcessingService fileProcessingService,
    ILoggerService loggerService) : IFileWatchingService
{
    private async Task OnCreatedAsync(object _, FileSystemEventArgs e)
    {
        try
        {
            await fileProcessingService.ProcessAsync(e);
        }
        catch (Exception ex)
        {
            loggerService.Log($"[MessageWatchingService]: An exception occurred - {ex.Message}", LogType.Error);
        }
    }
    
    public async Task Watch()
    {
        var fileSystemWatcher = new FileSystemWatcher
        {
            Path = Constants.Directories.Processing,
            Filter = "*.txt",
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };
        
        fileSystemWatcher.Created += (s, e) => _ = OnCreatedAsync(s, e);
        loggerService.Log("[MessageWatchingService]: Watching...", LogType.Information);
        
        await Task.Delay(-1);
    }
}