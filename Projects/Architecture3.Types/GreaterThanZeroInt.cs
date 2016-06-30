﻿namespace Architecture3.Types
{
    using CSharpFunctionalExtensions;

    public class GreaterThanZeroInt : ValueObject<GreaterThanZeroInt>
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

        public static Result<GreaterThanZeroInt> Create(int value)
        {
            return value <= 0 ? Result.Fail<GreaterThanZeroInt>("Value can't be lower or equal to zero") : Result.Ok(new GreaterThanZeroInt(value));
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