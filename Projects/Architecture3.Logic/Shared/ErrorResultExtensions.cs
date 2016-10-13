namespace Architecture3.Logic.Shared
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public static class ErrorResultExtensions
    {
        public static IResult<Error> ToGeneric(this NonEmptyString message)
        {
            return Result<Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToGeneric<T>(this NonEmptyString message)
        {
            return Result<T, Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToNotFound<T>()
        {
            return Result<T, Error>.Fail(Error.CreateNotFound());
        }

        public static IResult<Error> ToNotFound()
        {
            return Result<Error>.Fail(Error.CreateNotFound());
        }

        public static IResult<Error> ToPreconditionFailed()
        {
            return Result<Error>.Fail(Error.CreatePreconditionFailed());
        }

        public static IResult<T, Error> ToPreconditionFailed<T>()
        {
            return Result<T, Error>.Fail(Error.CreatePreconditionFailed());
        }
    }
}
