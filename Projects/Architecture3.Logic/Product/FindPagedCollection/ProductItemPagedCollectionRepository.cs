namespace Architecture3.Logic.Product.FindPagedCollection
{
    using Architecture3.Common.SharedStructs.ResponseParams;
    using Architecture3.Common.TemplateMethods.Queries.Interfaces;
    using Architecture3.WebApi.Dtos.Product.FindPagedCollection;

    public class ProductItemPagedCollectionRepository : IPagedCollectionRepository<ProductItem, Query>
    {
        public PagedCollectionResult<ProductItem> Fetch(Query query)
        {
            throw new System.NotImplementedException();
        }
    }
}