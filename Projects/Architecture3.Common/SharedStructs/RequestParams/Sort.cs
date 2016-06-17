namespace Architecture3.Common.SharedStructs.RequestParams
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.SharedStructs.ResponseParams;

    public class Sort<TItem> : IRequest<CollectionResult<TItem>>
    {
        public string SortExp { get; set; }
    }
}