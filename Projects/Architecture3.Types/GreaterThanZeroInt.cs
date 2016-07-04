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
            return Create(value).Value;
        }

        public static implicit operator int(GreaterThanZeroInt greaterThanZeroInt)
        {
            return greaterThanZeroInt.Value;
        }

        public static Result<GreaterThanZeroInt, string> Create(int value)
        {
            return value <= 0 ? Result<GreaterThanZeroInt, string>.Fail("Value can't be lower or equal to zero") : Result<GreaterThanZeroInt, string>.Ok(new GreaterThanZeroInt(value));
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