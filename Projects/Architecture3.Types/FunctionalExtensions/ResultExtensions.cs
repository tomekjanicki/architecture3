namespace Architecture3.Types.FunctionalExtensions
{
    using System;
    using System.Linq;

    public static class ResultExtensions
    {
        public static void EnsureIsNotFaliure<TResult>(this IResult<TResult, string> result)
        {
            EnsureIsNotFaliure((IResult<string>)result);
        }

        public static void EnsureIsNotFaliure<TResult, TError>(this IResult<TResult, TError> result, Func<string> messageFunc)
            where TError : class
        {
            EnsureIsNotFaliure((IResult<TError>)result, messageFunc);
        }

        public static void EnsureIsNotFaliure(this IResult<string> result)
        {
            EnsureIsNotFaliure(result, () => result.Error);
        }

        public static void EnsureIsNotFaliure<TError>(this IResult<TError> result, Func<string> messageFunc)
            where TError : class
        {
            if (result.IsFailure)
            {
                throw new InvalidOperationException(messageFunc());
            }
        }

        public static IResult<string> CombineFailures(IResult<string>[] results)
        {
            var failedResults = results.Where(result => result != null && result.IsFailure).ToList();

            if (!failedResults.Any())
            {
                return Result<string>.Ok();
            }

            var errorMessage = string.Join("; ", failedResults.Select(x => x.Error).ToArray());

            return Result<string>.Fail(errorMessage);
        }
    }
}
