namespace Architecture3.Common.TemplateMethods.Queries.Interfaces
{
    using Architecture3.Common.SharedStructs.RequestParams;
    using Architecture3.Common.SharedStructs.ResponseParams;

    public interface ICollectionRepository<TItem, in TParam>
        where TParam : Sort<TItem>
    {
        CollectionResult<TItem> Fetch(TParam query);
    }
}