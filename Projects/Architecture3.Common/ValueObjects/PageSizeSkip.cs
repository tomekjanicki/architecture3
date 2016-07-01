namespace Architecture3.Common.ValueObjects
{
    using Architecture3.Types;
    using CSharpFunctionalExtensions;

    public class PageSizeSkip : ValueObject<PageSizeSkip>
    {
        private PageSizeSkip(NonNegativeInt skip, GreaterThanZeroInt pageSize)
        {
            Skip = skip;
            PageSize = pageSize;
        }

        public NonNegativeInt Skip { get; }

        public GreaterThanZeroInt PageSize { get; }

        public static Result<PageSizeSkip> Create(int skip, int pageSize)
        {
            var r1 = NonNegativeInt.Create(skip);
            if (r1.IsFailure)
            {
                return Result.Fail<PageSizeSkip>(r1.Error);
            }

            var r2 = GreaterThanZeroInt.Create(pageSize);
            if (r2.IsFailure)
            {
                return Result.Fail<PageSizeSkip>(r2.Error);
            }

            return Create(r1.Value, r2.Value);
        }

        public static Result<PageSizeSkip> Create(NonNegativeInt skip, GreaterThanZeroInt pageSize)
        {
            return pageSize.Value > Const.MaxPageSize ? Result.Fail<PageSizeSkip>($"PageSize can't be greater than {Const.MaxPageSize}") : Result.Ok(new PageSizeSkip(skip, pageSize));
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
