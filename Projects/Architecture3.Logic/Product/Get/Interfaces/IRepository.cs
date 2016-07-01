namespace Architecture3.Logic.Product.Get.Interfaces
{
    public interface IRepository
    {
        Product Fetch(Query query);
    }
}