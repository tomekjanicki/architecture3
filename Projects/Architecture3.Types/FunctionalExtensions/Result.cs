namespace Architecture3.Types.FunctionalExtensions
{
    using System;

    public struct Result<TError> : IResult<TError>
        where TError : class
    {
        private readonly ResultCommonLogic<TError> _logic;

        private Result(bool isFailure, TError error)
        {
            _logic = new ResultCommonLogic<TError>(isFailure, error);
        }

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        public TError Error => _logic.Error;

        public static Result<TError> Ok()
        {
            return new Result<TError>(false, null);
        }

        public static Result<TError> Fail(TError error)
        {
            return new Result<TError>(true, error);
        }
    }

    public struct Result<TResult, TError> : IResult<TResult, TError>
        where TError : class
    {
        private readonly ResultCommonLogic<TError> _logic;
        private readonly TResult _value;

        internal Result(bool isFailure, TResult value, TError error)
        {
            if (!isFailure && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _logic = new ResultCommonLogic<TError>(isFailure, error);
            _value = value;
        }

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        public TError Error => _logic.Error;

        public TResult Value
        {
            get
            {
                if (!IsSuccess)
                {
                    throw new InvalidOperationException("There is no value for failure.");
                }

                return _value;
            }
        }

        public static Result<TResult, TError> Ok(TResult value)
        {
            return new Result<TResult, TError>(false, value, null);
        }

        public static Result<TResult, TError> Fail(TError error)
        {
            return new Result<TResult, TError>(true, default(TResult), error);
        }
    }
}
