namespace Architecture3.Logic.Facades
{
    using Architecture3.Logic.Facades.Interfaces;

    public class ProductsFilterPagedCriteriaProvider : IProductsFilterPagedCriteriaProvider
    {
        public ProductsFilterPagedCriteria Get(string filter, string orderBy)
        {
            return new ProductsFilterPagedCriteria(string.Empty, string.Empty, string.Empty);
        }
    }
}