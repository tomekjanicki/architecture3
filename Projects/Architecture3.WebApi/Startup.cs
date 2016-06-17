namespace Architecture3.WebApi
{
    using System.Web.Http;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration();
            RegisterRoutes.Execute(configuration);
            RegisterMiscs.Execute(configuration);
            appBuilder.UseWebApi(configuration);
            appBuilder.UseWelcomePage();
        }
    }
}