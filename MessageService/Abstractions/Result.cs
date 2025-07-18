namespace MessageService.Abstractions;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public object? ErrorObject { get; }

    protected Result(bool isSuccess, string? error)
    {
        if ((isSuccess && error != null) || (!isSuccess && error == null))
            throw new InvalidOperationException("Invalid result state.");
        
        IsSuccess = isSuccess;
        Error = error;
        ErrorObject = new { message = error };
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string? error) => new(false, error);
    public static Result<T> Success<T>(T value) => new(true, null, value);
    public static Result<T> Failure<T>(string? error = null) => new(false, error, default!);
}

public class Result<T>(bool isSuccess, string? error, T value) : Result(isSuccess, error)
{
    public T Value { get; private init; } = value;
}