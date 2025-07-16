using LoggingService.Contracts.IServices;
using Shared.Enums;

namespace LoggingService.Services;

public class LoggerService : ILoggerService
{
    public void Log(string message, LogType logType)
    {
        Console.ForegroundColor = logType switch
        {
            LogType.Success => ConsoleColor.Green,
            LogType.Error => ConsoleColor.Red,
            LogType.Warning => ConsoleColor.Yellow,
            _ => ConsoleColor.White
        };
        
        Console.WriteLine(message);
        Console.ResetColor();
    }
}