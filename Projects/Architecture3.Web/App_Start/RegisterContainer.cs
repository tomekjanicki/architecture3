namespace Architecture3.Web
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using Architecture3.Common.Handlers;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.Tools;
    using Architecture3.Common.Tools.Interfaces;
    using Architecture3.Logic;
    using Architecture3.Logic.CQ.Apis.Product;
    using Architecture3.Logic.CQ.Apis.Product.Delete.Interfaces;
    using Architecture3.Logic.CQ.Apis.Product.Get;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Queries.Interfaces;
    using Architecture3.Logic.Database;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Logic.Facades.Apis;
    using Architecture3.Logic.Facades.Pages;
    using AutoMapper;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Integration.WebApi;
    using Command = Architecture3.Logic.CQ.Apis.Product.Put.Command;
    using QueryHandler = Architecture3.Logic.CQ.Apis.Product.FilterPaged.QueryHandler;
    using Repository = Architecture3.Logic.CQ.Apis.Product.Delete.Repository;

    public static class RegisterContainer
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle(true);

            container.RegisterMvcControllers();

            container.RegisterWebApiControllers(configuration);

            RegisterScoped(container);

            RegisterSingletons(container);

            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
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
            container.Register(typeof(IVoidRequestHandler<>), assemblies, lifeStyle);
            container.Register<ProductsFilterPagedFacade>(lifeStyle);
            container.Register<ProductsGetFacade>(lifeStyle);
            container.Register<VersionGetFacade>(lifeStyle);
            container.Register<ProductsDeleteFacade>(lifeStyle);
            container.Register<ProductsPutFacade>(lifeStyle);
            container.Register<ProductsPostFacade>(lifeStyle);
            container.Register<HomeIndexFacade>(lifeStyle);
            container.Register<IRepository, Repository>(lifeStyle);
            container.Register<SharedQueries>(lifeStyle);
            container.Register<IUpdateRepository<Command>, Logic.CQ.Apis.Product.Put.Repository>(lifeStyle);
            container.Register<Logic.CQ.Apis.Product.Post.Interfaces.IRepository, Logic.CQ.Apis.Product.Post.Repository>(lifeStyle);
            container.Register<IGetRepository<Product>, Logic.CQ.Apis.Product.Get.Repository>(lifeStyle);
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