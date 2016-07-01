namespace Architecture3.Common.ValueObjects
{
    using CSharpFunctionalExtensions;

    public class SortPageSizeSkip : ValueObject<SortPageSizeSkip>
    {
        private SortPageSizeSkip(string sort, PageSizeSkip pageSizeSkip)
        {
            PageSizeSkip = pageSizeSkip;
            Sort = sort;
        }

        public string Sort { get; }

        public PageSizeSkip PageSizeSkip { get; }

        public static Result<SortPageSizeSkip> Create(string sort, int skip, int pageSize)
        {
            var r1 = PageSizeSkip.Create(skip, pageSize);
            return r1.IsFailure ? Result.Fail<SortPageSizeSkip>(r1.Error) : Result.Ok(new SortPageSizeSkip(sort, r1.Value));
        }

        protected override bool EqualsCore(SortPageSizeSkip other)
        {
            return PageSizeSkip == other.PageSizeSkip && Sort == other.Sort;
        }

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = hash * 7 + PageSizeSkip.GetHashCode();
            hash = hash * 7 + Sort.GetHashCode();
            return hash;
        }
    }
}