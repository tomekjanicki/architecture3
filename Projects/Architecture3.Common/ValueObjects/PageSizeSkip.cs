namespace Architecture3.Common.ValueObjects
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class PageSizeSkip : ValueObject<PageSizeSkip>
    {
        private PageSizeSkip(NonNegativeInt skip, GreaterThanZeroInt pageSize)
        {
            Skip = skip;
            PageSize = pageSize;
        }

        public NonNegativeInt Skip { get; }

        public GreaterThanZeroInt PageSize { get; }

        public static Result<PageSizeSkip, string> Create(int skip, int pageSize)
        {
            var skipResult = NonNegativeInt.Create(skip);
            if (skipResult.IsFailure)
            {
                return Result<PageSizeSkip, string>.Fail(skipResult.Error);
            }

            var pageSizeResult = GreaterThanZeroInt.Create(pageSize);
            if (pageSizeResult.IsFailure)
            {
                return Result<PageSizeSkip, string>.Fail(pageSizeResult.Error);
            }

            return Create(skipResult.Value, pageSizeResult.Value);
        }

        public static Result<PageSizeSkip, string> Create(NonNegativeInt skip, GreaterThanZeroInt pageSize)
        {
            return pageSize.Value > Const.MaxPageSize ? Result<PageSizeSkip, string>.Fail($"PageSize can't be greater than {Const.MaxPageSize}") : Result<PageSizeSkip, string>.Ok(new PageSizeSkip(skip, pageSize));
        }

        protected override bool EqualsCore(PageSizeSkip other)
        {
            return Skip == other.Skip && PageSize == other.PageSize;
        }

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = hash * 7 + Skip.GetHashCode();
            hash = hash * 7 + PageSize.GetHashCode();
            return hash;
        }
    }
}
