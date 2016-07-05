namespace Architecture3.Logic
{
    using Architecture3.WebApi.Dtos;
    using AutoMapper;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<Common.ValueObjects.Paged<CQ.Product.FilterPaged.Product>, Paged<WebApi.Dtos.Product.FilterPaged.Product>>();
            expression.CreateMap<CQ.Product.Get.Product, WebApi.Dtos.Product.Get.Product>();
        }
    }
}
