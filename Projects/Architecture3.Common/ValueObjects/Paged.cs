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

        public static Result<Paged<T>, string> Create(int count, IReadOnlyCollection<T> items)
        {
            var countResult = NonNegativeInt.Create(count, nameof(Count));
            var result = ResultExtensions.CombineFailures(new[]
            {
                countResult,
                items == null ? GetFailResult("Items can't be null") : (IResult<string>)null
            });

            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new Paged<T>(countResult.Value, items));
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
