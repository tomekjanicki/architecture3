namespace Architecture3.Logic
{
    using System.Collections.Generic;
    using Architecture3.Web.Dtos;
    using Architecture3.Web.Dtos.Product.FilterPaged;
    using AutoMapper;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<Common.ValueObjects.Paged<CQ.Product.FilterPaged.Product>, Paged<Product>>().ConvertUsing(new PagedConverter<CQ.Product.FilterPaged.Product, Product>());
            expression.CreateMap<CQ.Product.Get.Product, Web.Dtos.Product.Get.Product>();
            expression.CreateMap<CQ.Product.FilterPaged.Product, Product>();
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
