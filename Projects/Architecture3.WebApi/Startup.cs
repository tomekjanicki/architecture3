namespace Architecture3.Web
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
            RegisterMvcRoutes.Execute();
            RegisterWebApiRoutes.Execute(httpConfiguration);
            RegisterMvcMiscs.Execute();
            RegisterWebApiMiscs.Execute(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}