namespace Architecture3.Logic.CQ.Product.FilterPaged.Interfaces
{
    using Architecture3.Common.ValueObjects;

    public interface IRepository
    {
        Paged<Product> Fetch(Query query);
    }
}