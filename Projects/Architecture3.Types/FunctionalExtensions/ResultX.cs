namespace Architecture3.Types.FunctionalExtensions
{
    using System;

    public struct ResultX<TError> : IResultX<TError>
        where TError : class
    {
        private readonly ResultCommonLogicX<TError> _logic;

        private ResultX(bool isFailure, TError error)
        {
            _logic = new ResultCommonLogicX<TError>(isFailure, error);
        }

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        public TError Error => _logic.Error;

        public static ResultX<TError> Ok()
        {
            return new ResultX<TError>(false, null);
        }

        public static ResultX<TError> Fail(TError error)
        {
            return new ResultX<TError>(true, error);
        }
    }

    public struct ResultX<TResult, TError> : IResultX<TResult, TError>
        where TError : class
    {
        private readonly ResultCommonLogicX<TError> _logic;
        private readonly TResult _value;

        internal ResultX(bool isFailure, TResult value, TError error)
        {
            if (!isFailure && value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _logic = new ResultCommonLogicX<TError>(isFailure, error);
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

        public static ResultX<TResult, TError> Ok(TResult value)
        {
            return new ResultX<TResult, TError>(false, value, null);
        }

        public static ResultX<TResult, TError> Fail(TError error)
        {
            return new ResultX<TResult, TError>(true, default(TResult), error);
        }
    }
}
