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

            if (r2.Value > Const.MaxPageSize)
            {
                return Result.Fail<PageSizeSkip>($"PageSize can't be greater than {Const.MaxPageSize}");
            }

            return Result.Ok(new PageSizeSkip(r1.Value, r2.Value));
        }

        protected override bool EqualsCore(PageSizeSkip other)
        {
            return Skip == other.Skip && PageSize == other.PageSize;
        }

        protected override int GetHashCodeCore()
        {
            throw new System.NotImplementedException();
        }
    }
}
