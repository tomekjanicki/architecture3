namespace Architecture3.Logic
{
    using Architecture3.WebApi.Dtos;
    using AutoMapper;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<Paged<WebApi.Dtos.Product.FilterPaged.Product>, Common.ValueObjects.Paged<CQ.Product.FilterPaged.Product>>();
            expression.CreateMap<WebApi.Dtos.Product.Get.Product, CQ.Product.Get.Product>();
        }
    }
}
