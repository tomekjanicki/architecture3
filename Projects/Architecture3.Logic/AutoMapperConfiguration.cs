namespace Architecture3.Logic
{
    using System.Collections.Generic;
    using Architecture3.Web.Dtos;
    using Architecture3.Web.Dtos.Apis.Product.FilterPaged;
    using AutoMapper;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<Common.ValueObjects.Paged<CQ.Apis.Product.FilterPaged.Product>, Paged<Product>>().ConvertUsing(new PagedConverter<CQ.Apis.Product.FilterPaged.Product, Product>());
            expression.CreateMap<CQ.Apis.Product.Get.Product, Web.Dtos.Apis.Product.Get.Product>();
            expression.CreateMap<CQ.Apis.Product.FilterPaged.Product, Product>();
        }

        public class PagedConverter<TSource, TDestination> : ITypeConverter<Common.ValueObjects.Paged<TSource>, Paged<TDestination>>
        {
            public Paged<TDestination> Convert(Common.ValueObjects.Paged<TSource> source, Paged<TDestination> destination, ResolutionContext context)
            {
                return new Paged<TDestination>(source.Count, context.Mapper.Map<IEnumerable<TDestination>>(source.Items));
            }
        }
    }
}
