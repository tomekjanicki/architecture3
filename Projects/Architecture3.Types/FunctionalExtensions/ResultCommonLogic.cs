namespace Architecture3.Types.FunctionalExtensions
{
    using System;
    using NullGuard;

    internal sealed class ResultCommonLogic<TError>
        where TError : class
    {
        private readonly TError _error;

        public ResultCommonLogic(bool isFailure, [AllowNull]TError error)
        {
            if (isFailure)
            {
                if (error == null)
                {
                    throw new ArgumentNullException(nameof(error), "There must be error for failure.");
                }
            }
            else
            {
                if (error != null)
                {
                    throw new ArgumentException("There should be no error for success.", nameof(error));
                }
            }

            IsFailure = isFailure;
            _error = error;
        }

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        public TError Error
        {
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException("There is no error for success.");
                }

                return _error;
            }
        }
    }
}
