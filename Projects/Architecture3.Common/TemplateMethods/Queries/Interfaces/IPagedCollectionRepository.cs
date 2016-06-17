namespace Architecture3.Common.TemplateMethods.Queries.Interfaces
{
    using Architecture3.Common.SharedStructs.RequestParams;
    using Architecture3.Common.SharedStructs.ResponseParams;

    public interface IPagedCollectionRepository<TItem, in TParam>
        where TParam : SortPageSizeSkip<TItem>
    {
        PagedCollectionResult<TItem> Fetch(TParam query);
    }
}