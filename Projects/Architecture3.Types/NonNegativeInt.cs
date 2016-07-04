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
            return Create(value).Value;
        }

        public static implicit operator int(NonNegativeInt nonNegativeInt)
        {
            return nonNegativeInt.Value;
        }

        public static Result<NonNegativeInt, string> Create(int value)
        {
            return value < 0 ? Result<NonNegativeInt, string>.Fail("Value can't be lower than zero") : Result<NonNegativeInt, string>.Ok(new NonNegativeInt(value));
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
