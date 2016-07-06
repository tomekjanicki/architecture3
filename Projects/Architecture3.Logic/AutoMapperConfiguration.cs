namespace Architecture3.Logic
{
    using System.Collections.Generic;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Dtos.Product.FilterPaged;
    using AutoMapper;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<Common.ValueObjects.Paged<CQ.Product.FilterPaged.Product>, Paged<WebApi.Dtos.Product.FilterPaged.Product>>().ConvertUsing(new TypeTypeConverter());
            expression.CreateMap<CQ.Product.Get.Product, WebApi.Dtos.Product.Get.Product>();
            expression.CreateMap<CQ.Product.FilterPaged.Product, WebApi.Dtos.Product.FilterPaged.Product>();
        }

        public class TypeTypeConverter : ITypeConverter<Common.ValueObjects.Paged<CQ.Product.FilterPaged.Product>, Paged<WebApi.Dtos.Product.FilterPaged.Product>>
        {
            public Paged<WebApi.Dtos.Product.FilterPaged.Product> Convert(Common.ValueObjects.Paged<CQ.Product.FilterPaged.Product> source, ResolutionContext context)
            {
                return new Paged<Product>(source.Count, context.Mapper.Map<IEnumerable<WebApi.Dtos.Product.FilterPaged.Product>>(source.Items));
            }
        }
    }
}
