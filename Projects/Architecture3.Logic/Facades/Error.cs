namespace Architecture3.Logic.Facades
{
    public struct Error
    {
        private Error(ErrorType errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }

        public ErrorType ErrorType { get; }

        public string Message { get; }

        public static Error CreateBadRequest(string message)
        {
            return new Error(ErrorType.BadRequest, message);
        }

        public static Error CreateNotFound()
        {
            return new Error(ErrorType.NotFound, null);
        }
    }
}