namespace Architecture3.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Architecture3.Common.Handlers;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.Tools;
    using Architecture3.Common.Tools.Interfaces;
    using Architecture3.Logic;
    using Architecture3.Logic.CQ.Product;
    using Architecture3.Logic.CQ.Product.Delete;
    using Architecture3.Logic.CQ.Product.Delete.Interfaces;
    using Architecture3.Logic.CQ.Product.FilterPaged;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Queries.Interfaces;
    using Architecture3.Logic.Database;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Logic.Facades;
    using AutoMapper;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class RegisterContainer
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle(true);

            container.RegisterWebApiControllers(configuration);

            RegisterScoped(container);

            RegisterSingletons(container);

            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterSingletons(Container container)
        {
            container.RegisterSingleton<IMediator, Mediator>();
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton<IAssemblyVersionProvider, AssemblyVersionProvider>();
            container.RegisterSingleton<IDbConnectionProvider, DbConnectionProvider>();
            container.RegisterSingleton(GetMapper);
        }

        private static void RegisterScoped(Container container)
        {
            var assemblies = GetAssemblies().ToArray();
            var lifeStyle = Lifestyle.Scoped;
            container.Register(typeof(IRequestHandler<,>), assemblies, lifeStyle);
            container.Register<ProductsFilterPagedFacade>(lifeStyle);
            container.Register<ProductsGetFacade>(lifeStyle);
            container.Register<VersionGetFacade>(lifeStyle);
            container.Register<ProductsDeleteFacade>(lifeStyle);
            container.Register<ProductsPutFacade>(lifeStyle);
            container.Register<ProductsPostFacade>(lifeStyle);
            container.Register<IRepository, Repository>(lifeStyle);
            container.Register<SharedQueries>(lifeStyle);
            container.Register<IUpdateRepository<Logic.CQ.Product.Put.Command>, Logic.CQ.Product.Put.Repository>(lifeStyle);
            container.Register<Logic.CQ.Product.Post.Interfaces.IRepository, Logic.CQ.Product.Post.Repository>(lifeStyle);
            container.Register<IGetRepository<Logic.CQ.Product.Get.Product>, Logic.CQ.Product.Get.Repository>(lifeStyle);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(QueryHandler).GetTypeInfo().Assembly;
        }

        private static IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(AutoMapperConfiguration.Configure);
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}