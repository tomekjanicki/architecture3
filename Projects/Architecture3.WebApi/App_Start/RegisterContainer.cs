namespace Architecture3.WebApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Architecture3.Common.Handlers;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic;
    using Architecture3.Logic.Facades;
    using Architecture3.Logic.Interfaces;
    using Architecture3.Logic.Product.FilterPaged;
    using Architecture3.Logic.Product.FilterPaged.Interfaces;
    using Architecture3.WebApi.Dtos;
    using AutoMapper;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class RegisterContainer
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var lifeStyle = Lifestyle.Scoped;

            var container = new Container();

            var assemblies = GetAssemblies().ToArray();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle(true);

            container.RegisterWebApiControllers(configuration);

            container.RegisterSingleton<IMediator, Mediator>();

            container.Register(typeof(IRequestHandler<,>), assemblies, lifeStyle);

            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));

            container.Register<IRepository, Repository>(lifeStyle);

            container.Register<Logic.Product.Get.Interfaces.IRepository, Logic.Product.Get.Repository>(lifeStyle);

            container.Register<IDbConnectionProvider, DbConnectionProvider>(lifeStyle);

            container.Register<FilterPagedFacade>(lifeStyle);

            container.Register<ProductsGetFacade>(lifeStyle);

            container.RegisterSingleton(GetMapper);

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            container.Verify();
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(QueryHandler).GetTypeInfo().Assembly;
        }

        private static IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(expression =>
            {
                expression.CreateMap<Common.ValueObjects.Paged<Product>, Paged<WebApi.Dtos.Product.FilterPaged.Product>>();
                expression.CreateMap<Logic.Product.Get.Product, WebApi.Dtos.Product.Get.Product>();
            });
            return configuration.CreateMapper();
        }
    }
}