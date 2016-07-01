namespace Architecture3.Logic.Product.Get.Interfaces
{
    public interface IResultMapper
    {
        WebApi.Dtos.Product.Get.Product Map(Get.Product input);
    }
}