namespace Architecture3.Web.Infrastructure
{
    using System.Web.Http.ExceptionHandling;
    using Architecture3.Common.Log4Net;
    using log4net;

    public sealed class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalExceptionLogger));

        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Error(() => "An unhandled exception has occured", () => context.Exception);
        }
    }
}