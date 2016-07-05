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
            return Create(value, "Value").Value;
        }

        public static implicit operator decimal(NonNegativeDecimal nonNegativeInt)
        {
            return nonNegativeInt.Value;
        }

        public static Result<NonNegativeDecimal, string> Create(decimal? value, string field)
        {
            return value == null ? GetFailResult("{0} can't be null", field) : Create(value.Value, field);
        }

        public static Result<NonNegativeDecimal, string> Create(decimal value, string field)
        {
            return value < 0 ? GetFailResult("{0} can't be lower than zero", field) : GetOkResult(new NonNegativeDecimal(value));
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