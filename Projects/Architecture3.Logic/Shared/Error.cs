namespace Architecture3.Logic.Shared
{
    using System;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public class Error : ValueObject<Error>
    {
        private readonly string _message;

        private Error(ErrorType errorType, string message)
        {
            ErrorType = errorType;
            _message = message;
        }

        public ErrorType ErrorType { get; }

        public string Message
        {
            get
            {
                if (ErrorType != ErrorType.Generic)
                {
                    throw new InvalidOperationException($"There is no message for others than {ErrorType.Generic}.");
                }

                return _message;
            }
        }

        public static Error CreateGeneric(NonEmptyString message)
        {
            return new Error(ErrorType.Generic, message);
        }

        public static Error CreateNotFound()
        {
            return new Error(ErrorType.NotFound, string.Empty);
        }

        public static Error CreatePreconditionFailed()
        {
            return new Error(ErrorType.PreconditionFailed, string.Empty);
        }

        protected override bool EqualsCore(Error other)
        {
            return ErrorType == other.ErrorType && _message == other._message;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { ErrorType, _message });
        }
    }
}