namespace Architecture3.Logic.Product.Get.Interfaces
{
    using Architecture3.Types.FunctionalExtensions;

    public interface IRepository
    {
        Maybe<Product> Fetch(Query query);
    }
}