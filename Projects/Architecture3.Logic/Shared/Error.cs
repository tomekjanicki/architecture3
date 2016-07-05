namespace Architecture3.Logic.Shared
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public class Error : ValueObject<Error>
    {
        private Error(ErrorType errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }

        public ErrorType ErrorType { get; }

        public string Message { get; }

        public static Error CreateBadRequest(NonEmptyString message)
        {
            return new Error(ErrorType.BadRequest, message);
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
            return ErrorType == other.ErrorType && Message == other.Message;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { ErrorType, Message });
        }
    }
}