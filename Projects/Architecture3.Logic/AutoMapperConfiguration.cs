namespace Architecture3.Logic
{
    using Architecture3.WebApi.Dtos;
    using AutoMapper;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<Common.ValueObjects.Paged<Product.FilterPaged.Product>, Paged<WebApi.Dtos.Product.FilterPaged.Product>>();
            expression.CreateMap<Logic.Product.Get.Product, WebApi.Dtos.Product.Get.Product>();
        }
    }
}
