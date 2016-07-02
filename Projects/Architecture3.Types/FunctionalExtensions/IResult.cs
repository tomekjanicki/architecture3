namespace Architecture3.Types.FunctionalExtensions
{
    public interface IResult<out TResult, out TError> : IResultX<TError>
        where TError : class
    {
        TResult Value { get; }
    }

    public interface IResultX<out TError>
        where TError : class
    {
        bool IsFailure { get; }

        bool IsSuccess { get; }

        TError Error { get; }
    }
}