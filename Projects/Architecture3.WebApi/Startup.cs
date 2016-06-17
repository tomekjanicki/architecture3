namespace Architecture3.WebApi
{
    using System.Web.Http;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            RegisterContainer.Execute(httpConfiguration);
            RegisterSwagger.Execute(httpConfiguration);
            RegisterRoutes.Execute(httpConfiguration);
            RegisterMiscs.Execute(httpConfiguration);
            RegisterValidation.Execute();
            appBuilder.UseWebApi(httpConfiguration);
            appBuilder.UseWelcomePage();
        }
    }
}