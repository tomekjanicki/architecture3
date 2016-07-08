﻿namespace Architecture3.Logic.Shared
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public static class ErrorResultExtensions
    {
        public static IResult<Error> ToBadRequest(this NonEmptyString message)
        {
            return Result<Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToBadRequest<T>(this NonEmptyString message)
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
    }
}
