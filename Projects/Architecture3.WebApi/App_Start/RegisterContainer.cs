namespace Architecture3.WebApi
{
    using System.Web.Http;
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

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle(true);

            container.RegisterWebApiControllers(configuration);

            container.Register<IResultMapper, ResultMapper>(lifeStyle);

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            container.Verify();
        }
    }
}