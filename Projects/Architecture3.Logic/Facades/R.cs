namespace Architecture3.Logic.Facades
{
    public struct R<TValue, TErrorResult>
    {
        private R(TValue value, TErrorResult errorResult)
        {
            Value = value;
            ErrorResult = errorResult;
        }

        public TErrorResult ErrorResult { get; }

        public TValue Value { get; }

        public static R<TValue, TErrorResult> Ok(TValue value)
        {
            return new R<TValue, TErrorResult>(value, default(TErrorResult));
        }

        public static R<TValue, TErrorResult> Fail(TErrorResult errorResult)
        {
            return new R<TValue, TErrorResult>(default(TValue), errorResult);
        }
    }
}