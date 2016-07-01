namespace Architecture3.Logic.Product.Get
{
    using Architecture3.Logic.Product.Get.Interfaces;

    public class ResultMapper : IResultMapper
    {
        public WebApi.Dtos.Product.Get.Product Map(Get.Product input)
        {
            return new WebApi.Dtos.Product.Get.Product
            {
                Code = input.Code,
                CanDelete = input.CanDelete,
                Date = input.Date,
                Id = input.Id,
                Name = input.Name,
                Price = input.Price,
                Version = input.Version
            };
        }
    }
}
