using Shared.Enums;

namespace LoggingService.Contracts.IServices;

public interface ILoggerService
{
    void Log(string message, LogType logType);
}