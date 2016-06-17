namespace Architecture3.Common.SharedStructs.ResponseParams
{
    public class PagedCollectionResult<TItem> : Result<Paged<TItem>>
    {
        public PagedCollectionResult(Paged<TItem> results)
            : base(results)
        {
        }
    }
}