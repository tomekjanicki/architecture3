namespace Architecture3.Logic.CQ.Product.Get.Interfaces
{
    using Architecture3.Types.FunctionalExtensions;

    public interface IRepository
    {
        Maybe<CQ.Product.Get.Product> Fetch(Query query);
    }
}