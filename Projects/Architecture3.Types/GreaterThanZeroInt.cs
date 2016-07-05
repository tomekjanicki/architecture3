namespace Architecture3.Types
{
    using Architecture3.Types.FunctionalExtensions;

    public sealed class GreaterThanZeroInt : ValueObject<GreaterThanZeroInt>
    {
        private GreaterThanZeroInt(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static explicit operator GreaterThanZeroInt(int value)
        {
            return Create(value, (NonEmptyString)"Value").Value;
        }

        public static implicit operator int(GreaterThanZeroInt greaterThanZeroInt)
        {
            return greaterThanZeroInt.Value;
        }

        public static Result<GreaterThanZeroInt, NonEmptyString> Create(int? value, NonEmptyString field)
        {
            return value == null ? GetFailResult((NonEmptyString)"{0} can't be null", field) : Create(value.Value, field);
        }

        public static Result<GreaterThanZeroInt, NonEmptyString> Create(int value, NonEmptyString field)
        {
            return value <= 0 ? GetFailResult((NonEmptyString)"{0} can't be lower or equal to zero", field) : GetOkResult(new GreaterThanZeroInt(value));
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override bool EqualsCore(GreaterThanZeroInt other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}