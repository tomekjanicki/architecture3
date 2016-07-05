namespace Architecture3.Logic.Shared
{
    using Architecture3.Types.FunctionalExtensions;

    public static class ErrorResultExtensions
    {
        public static Result<Error> ToBadRequest(this string message)
        {
            return Result<Error>.Fail(Error.CreateBadRequest(message));
        }

        public static Result<T, Error> ToBadRequest<T>(this string message)
        {
            return Result<T, Error>.Fail(Error.CreateBadRequest(message));
        }

        public static Result<T, Error> ToNotFound<T>()
        {
            return Result<T, Error>.Fail(Error.CreateNotFound());
        }

        public static Result<Error> ToNotFound()
        {
            return Result<Error>.Fail(Error.CreateNotFound());
        }

        public static Result<Error> ToPreconditionFailed()
        {
            return Result<Error>.Fail(Error.CreatePreconditionFailed());
        }
    }
}
