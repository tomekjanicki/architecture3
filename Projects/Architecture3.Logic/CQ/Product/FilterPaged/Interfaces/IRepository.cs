namespace Architecture3.Logic.CQ.Product.FilterPaged.Interfaces
{
    using Architecture3.Common.ValueObjects;

    public interface IRepository
    {
        Paged<CQ.Product.FilterPaged.Product> Fetch(Query query);
    }
}