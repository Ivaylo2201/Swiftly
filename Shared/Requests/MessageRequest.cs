﻿namespace Shared.Requests;

public record MessageRequest
{
    public required string Message { get; init; }
    public required int Index { get; init; }
}