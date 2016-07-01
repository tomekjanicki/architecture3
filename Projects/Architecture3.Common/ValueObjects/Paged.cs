namespace Architecture3.Common.ValueObjects
{
    using System.Collections.Generic;
    using Architecture3.Types;
    using CSharpFunctionalExtensions;

    public class Paged<T> : ValueObject<Paged<T>>
    {
        private Paged(NonNegativeInt count, IReadOnlyCollection<T> items)
        {
            Count = count;
            Items = items;
        }

        public NonNegativeInt Count { get; }

        public IReadOnlyCollection<T> Items { get; }

        public static Result<Paged<T>> Create(int count, IReadOnlyCollection<T> items)
        {
            var countResult = NonNegativeInt.Create(count);
            if (countResult.IsFailure)
            {
                return Result.Fail<Paged<T>>(countResult.Error);
            }

            if (items == null)
            {
                return Result.Fail<Paged<T>>($"{nameof(items)} can't be null");
            }

            return Result.Ok(new Paged<T>(countResult.Value, items));
        }

        protected override bool EqualsCore(Paged<T> other)
        {
            return Count == other.Count && Equals(Items, other.Items);
        }

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = hash * 7 + Count.GetHashCode();
            hash = hash * 7 + Items.GetHashCode();
            return hash;
        }
    }
}
