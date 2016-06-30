namespace Architecture3.Types
{
    using CSharpFunctionalExtensions;

    public class NonNegativeInt : ValueObject<NonNegativeInt>
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

        public static Result<NonNegativeInt> Create(int value)
        {
            return value < 0 ? Result.Fail<NonNegativeInt>("Value can't be lower than zero") : Result.Ok(new NonNegativeInt(value));
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
