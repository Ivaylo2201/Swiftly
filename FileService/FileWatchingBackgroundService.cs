using FileService.Contracts.IServices;
using Microsoft.Extensions.Hosting;

namespace FileService;

public class FileWatchingBackgroundService(IFileWatchingService fileWatchingService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await fileWatchingService.Watch(stoppingToken);
    }
}