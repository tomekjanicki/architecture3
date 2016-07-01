namespace Architecture3.WebApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using Architecture3.Common.Handlers;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic;
    using Architecture3.Logic.Interfaces;
    using Architecture3.Logic.Product.FilterPaged;
    using Architecture3.Logic.Product.FilterPaged.Interfaces;
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

            container.Register<IResultMapper, ResultMapper>(lifeStyle);

            container.Register<IRepository, Repository>(lifeStyle);

            container.Register<IDbConnectionProvider, DbConnectionProvider>(lifeStyle);

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            container.Verify();
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(QueryHandler).GetTypeInfo().Assembly;
        }
    }
}