using Shared.Enums;

namespace Shared.Requests;

public record LoggingRequest
{
    public required string Message { get; init; }
    public required LogType LogType { get; init; }
};