namespace Architecture3.Common.ValueObjects
{
    using Architecture3.Types.FunctionalExtensions;

    public class SortPageSizeSkip : ValueObject<SortPageSizeSkip>
    {
        private SortPageSizeSkip(string sort, PageSizeSkip pageSizeSkip)
        {
            PageSizeSkip = pageSizeSkip;
            Sort = sort;
        }

        public string Sort { get; }

        public PageSizeSkip PageSizeSkip { get; }

        public static ResultX<SortPageSizeSkip, string> Create(string sort, int skip, int pageSize)
        {
            var result = PageSizeSkip.Create(skip, pageSize);
            return result.IsFailure ? ResultX<SortPageSizeSkip, string>.Fail(result.Error) : ResultX<SortPageSizeSkip, string>.Ok(new SortPageSizeSkip(sort, result.Value));
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