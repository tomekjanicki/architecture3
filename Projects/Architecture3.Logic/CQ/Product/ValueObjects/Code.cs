namespace Architecture3.Logic.CQ.Product.ValueObjects
{
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Code : ValueObject<Code>
    {
        private Code(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static explicit operator Code(string value)
        {
            return Create(value, "Value").Value;
        }

        public static implicit operator string(Code code)
        {
            return code.Value;
        }

        public static Result<Code, string> Create(string code, string field)
        {
            if (string.IsNullOrEmpty(code))
            {
                return GetFailResult("{0} can't be null or empty", field);
            }

            const int max = 50;

            return code.Length > max ? GetFailResult($"{0} can't be longer than {max} chars.", field) : GetOkResult(new Code(code));
        }

        protected override bool EqualsCore(Code other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}