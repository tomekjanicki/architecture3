namespace Architecture3.WebApi.Infrastructure
{
    using System.Web.Http.ExceptionHandling;
    using FluentValidation;
    using log4net;

    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalExceptionLogger));

        public override void Log(ExceptionLoggerContext context)
        {
            var validationException = context.Exception as ValidationException;

            if (validationException != null)
            {
                Logger.Warn("Validation errors", validationException);
            }
            else
            {
                Logger.Error("An unhandled exception has occured", context.Exception);
            }
        }
    }
}