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
            return Create(value).Value;
        }

        public static implicit operator string(Code name)
        {
            return name.Value;
        }

        public static Result<Code, string> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result<Code, string>.Fail("Code can't be empty");
            }

            const int max = 50;

            return name.Length > max ? Result<Code, string>.Fail($"Code can't be longer than {max} chars.") : Result<Code, string>.Ok(new Code(name));
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