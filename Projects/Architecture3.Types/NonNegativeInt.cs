namespace Architecture3.Types
{
    using Architecture3.Types.FunctionalExtensions;

    public sealed class NonNegativeInt : ValueObject<NonNegativeInt>
    {
        private NonNegativeInt(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static explicit operator NonNegativeInt(int value)
        {
            return Create(value, (NonEmptyString)"Value").Value;
        }

        public static implicit operator int(NonNegativeInt nonNegativeInt)
        {
            return nonNegativeInt.Value;
        }

        public static IResult<NonNegativeInt, NonEmptyString> Create(int? value, NonEmptyString field)
        {
            return value == null ? GetFailResult((NonEmptyString)"{0} can't be null", field) : Create(value.Value, field);
        }

        public static IResult<NonNegativeInt, NonEmptyString> Create(int value, NonEmptyString field)
        {
            return value < 0 ? GetFailResult((NonEmptyString)"{0} can't be lower than zero", field) : GetOkResult(new NonNegativeInt(value));
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override bool EqualsCore(NonNegativeInt other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
