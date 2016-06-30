namespace Architecture3.WebApi
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class RegisterContainer
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle(true);

            container.RegisterWebApiControllers(configuration);

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            container.Verify();
        }
    }
}