namespace Architecture3.Common.ValueObjects
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class TopSkip : ValueObject<TopSkip>
    {
        private TopSkip(NonNegativeInt skip, GreaterThanZeroInt top)
        {
            Skip = skip;
            Top = top;
        }

        public NonNegativeInt Skip { get; }

        public GreaterThanZeroInt Top { get; }

        public static Result<TopSkip, string> Create(int skip, int pageSize)
        {
            var skipResult = NonNegativeInt.Create(skip);
            if (skipResult.IsFailure)
            {
                return Result<TopSkip, string>.Fail(skipResult.Error);
            }

            var pageSizeResult = GreaterThanZeroInt.Create(pageSize);

            return pageSizeResult.IsFailure ? Result<TopSkip, string>.Fail(pageSizeResult.Error) : Create(skipResult.Value, pageSizeResult.Value);
        }

        public static Result<TopSkip, string> Create(NonNegativeInt skip, GreaterThanZeroInt pageSize)
        {
            return pageSize.Value > Const.MaxTopSize ? Result<TopSkip, string>.Fail($"Top can't be greater than {Const.MaxTopSize}") : Result<TopSkip, string>.Ok(new TopSkip(skip, pageSize));
        }

        protected override bool EqualsCore(TopSkip other)
        {
            return Skip == other.Skip && Top == other.Top;
        }

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = hash * 7 + Skip.GetHashCode();
            hash = hash * 7 + Top.GetHashCode();
            return hash;
        }
    }
}
