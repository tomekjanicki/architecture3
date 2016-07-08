namespace Architecture3.Common.ValueObjects
{
    using System.Collections.Generic;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Paged<T> : ValueObject<Paged<T>>
    {
        private Paged(NonNegativeInt count, IReadOnlyCollection<T> items)
        {
            Count = count;
            Items = items;
        }

        public NonNegativeInt Count { get; }

        public IReadOnlyCollection<T> Items { get; }

        public static IResult<Paged<T>, NonEmptyString> Create(int count, IReadOnlyCollection<T> items)
        {
            var countResult = NonNegativeInt.Create(count, (NonEmptyString)nameof(Count));
            return countResult.IsFailure ? GetFailResult(countResult.Error) : GetOkResult(new Paged<T>(countResult.Value, items));
        }

        public static Paged<T> CreateAndEnsureIsNotFaliure(int count, IReadOnlyCollection<T> items)
        {
            var pagedResult = Create(count, items);
            pagedResult.EnsureIsNotFaliure();
            return pagedResult.Value;
        }

        protected override bool EqualsCore(Paged<T> other)
        {
            return Count == other.Count && Equals(Items, other.Items);
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Count, Items });
        }
    }
}
