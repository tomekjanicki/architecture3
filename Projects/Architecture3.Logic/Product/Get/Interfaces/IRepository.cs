namespace Architecture3.Logic.Product.Get.Interfaces
{
    using CSharpFunctionalExtensions;

    public interface IRepository
    {
        Maybe<Product> Fetch(Query query);
    }
}