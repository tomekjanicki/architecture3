namespace Architecture3.Logic.Product.FindPagedCollection
{
    using Architecture3.Common.SharedStructs.ResponseParams;
    using Architecture3.Common.TemplateMethods.Queries.Interfaces;

    public class ProductItemPagedCollectionRepository : IPagedCollectionRepository<WebApi.Dtos.Product.FilterPaged.Product, Query>
    {
        public PagedCollectionResult<WebApi.Dtos.Product.FilterPaged.Product> Fetch(Query query)
        {
            throw new System.NotImplementedException();
        }
    }
}