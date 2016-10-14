namespace Architecture3.Web
{
    using System.Web.Http;
    using Architecture3.Common.Log4Net;
    using Architecture3.Web.Infrastructure;
    using log4net;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Owin;

    public class Startup
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Startup));

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(GlobalLoggerHttpModule));
        }

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
            Logger.Info(() => "Application started");
        }
    }
}