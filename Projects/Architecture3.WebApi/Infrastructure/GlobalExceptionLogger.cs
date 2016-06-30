namespace Architecture3.WebApi.Infrastructure
{
    using System.Web.Http.ExceptionHandling;
    using log4net;

    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalExceptionLogger));

        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Error("An unhandled exception has occured", context.Exception);
        }
    }
}