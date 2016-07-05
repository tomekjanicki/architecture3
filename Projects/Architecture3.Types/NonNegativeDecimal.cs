namespace Architecture3.Types
{
    using System.Globalization;
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
            return Create(value, (NonEmptyString)"Value").Value;
        }

        public static implicit operator decimal(NonNegativeDecimal nonNegativeInt)
        {
            return nonNegativeInt.Value;
        }

        public static Result<NonNegativeDecimal, NonEmptyString> Create(decimal? value, NonEmptyString field)
        {
            return value == null ? GetFailResult((NonEmptyString)"{0} can't be null", field) : Create(value.Value, field);
        }

        public static Result<NonNegativeDecimal, NonEmptyString> Create(decimal value, NonEmptyString field)
        {
            return value < 0 ? GetFailResult((NonEmptyString)"{0} can't be lower than zero", field) : GetOkResult(new NonNegativeDecimal(value));
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
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