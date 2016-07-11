namespace Architecture3.Web.Infrastructure
{
    using System;
    using System.Web;
    using Architecture3.Common.Log4Net;
    using log4net;

    public class GlobalLoggerHttpModule : IHttpModule
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalLoggerHttpModule));

        public void Init(HttpApplication context)
        {
            context.Error += Error;
        }

        public void Dispose()
        {
        }

        private static void Error(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var exception = application.Server.GetLastError();
            Logger.Error(() => "An unhandled exception has occured", () => exception);
        }
    }
}