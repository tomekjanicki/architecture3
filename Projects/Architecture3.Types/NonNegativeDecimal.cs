namespace Architecture3.Types
{
    using Architecture3.Types.FunctionalExtensions;

    public sealed class NonNegativeDecimal : ValueObject<NonNegativeDecimal>
    {
        private NonNegativeDecimal(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static explicit operator NonNegativeDecimal(decimal value)
        {
            return Create(value).Value;
        }

        public static implicit operator decimal(NonNegativeDecimal nonNegativeInt)
        {
            return nonNegativeInt.Value;
        }

        public static Result<NonNegativeDecimal, string> Create(decimal value)
        {
            return value < 0 ? Result<NonNegativeDecimal, string>.Fail("Value can't be lower than zero") : Result<NonNegativeDecimal, string>.Ok(new NonNegativeDecimal(value));
        }

        protected override bool EqualsCore(NonNegativeDecimal other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}