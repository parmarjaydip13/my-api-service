namespace SharedKernel;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(Result));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new Result(true, Error.None);

    public static Result Failure(Error error) => new Result(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

}


public class Result<Tvalue> : Result
{
    private readonly Tvalue? _value;

    protected internal Result(Tvalue? value, bool IsSuccess, Error error) : base(IsSuccess, error)
    {
        _value = value;
    }

    public Tvalue Value => IsSuccess ? _value! :
        throw new InvalidOperationException("The value of failure result can't be access");

    public static implicit operator Result<Tvalue>(Tvalue? value) =>
        value is not null ? Success(value) : Failure<Tvalue>(Error.NullValue);

}