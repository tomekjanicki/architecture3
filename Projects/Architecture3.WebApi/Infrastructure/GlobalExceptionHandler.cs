namespace Architecture3.WebApi.Infrastructure
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.Results;
    using FluentValidation;

    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var validationException = context.Exception as ValidationException;

            if (validationException != null)
            {
                var response = context.Request.CreateResponse(HttpStatusCode.BadRequest, validationException.Message);
                context.Result = new ResponseMessageResult(response);
            }

            base.Handle(context);
        }
    }
}