namespace Architecture3.Logic.Facades.Interfaces
{
    public interface IProductsFilterPagedCriteriaProvider
    {
        ProductsFilterPagedCriteria Get(string filter, string orderBy);
    }
}