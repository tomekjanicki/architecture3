namespace Architecture3.Types
{
    using Architecture3.Types.FunctionalExtensions;

    public sealed class NonEmptyString : ValueObject<NonEmptyString>
    {
        private NonEmptyString(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static explicit operator NonEmptyString(string value)
        {
            return Create(value, new NonEmptyString("Value")).Value;
        }

        public static implicit operator string(NonEmptyString value)
        {
            return value.Value;
        }

        public static Result<NonEmptyString, NonEmptyString> Create(string value, NonEmptyString field)
        {
            return value == string.Empty ? GetFailResult((NonEmptyString)"{0} can't be empty", field) : GetOkResult(new NonEmptyString(value));
        }

        public override string ToString()
        {
            return Value;
        }

        protected override bool EqualsCore(NonEmptyString other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
