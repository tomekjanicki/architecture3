namespace Architecture3.Logic.CQ.Apis.Product.ValueObjects
{
    using Architecture3.Types;
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
            return Create(value, (NonEmptyString)"Value").Value;
        }

        public static implicit operator string(Code code)
        {
            return code.Value;
        }

        public static IResult<Code, NonEmptyString> Create(string code, NonEmptyString field)
        {
            if (code == string.Empty)
            {
                return GetFailResult((NonEmptyString)"{0} can't be empty", field);
            }

            const int max = 50;

            return code.Length > max ? GetFailResult((NonEmptyString)$"{{0}} can't be longer than {max} chars.", field) : GetOkResult(new Code(code));
        }

        public override string ToString()
        {
            return Value;
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