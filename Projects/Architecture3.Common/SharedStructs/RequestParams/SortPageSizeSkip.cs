namespace Architecture3.Common.SharedStructs.RequestParams
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.SharedStructs.ResponseParams;

    public class SortPageSizeSkip<TItem> : IRequest<PagedCollectionResult<TItem>>
    {
        public string SortExp { get; set; }

        public int PageSize { get; set; }

        public int Skip { get; set; }
    }
}