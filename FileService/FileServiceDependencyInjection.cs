using FileService.Contracts.IServices;
using FileService.Services;
using Microsoft.Extensions.DependencyInjection;
using PersistenceService;
using PersistenceService.Contracts.IRepositories;
using PersistenceService.Repositories;

namespace FileService;

public static class FileServiceDependencyInjection
{
    public static void AddFileService(this IServiceCollection services)
    {
        services.AddScoped<IFileMovingService, FileMovingService>();
        services.AddScoped<IFileProcessingService, FileProcessingService>();
        services.AddScoped<IFileWatchingService, FileWatchingService>();
        services.AddPersistenceService();
    }
}