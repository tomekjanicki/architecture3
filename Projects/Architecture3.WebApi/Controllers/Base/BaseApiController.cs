namespace Architecture3.WebApi.Controllers.Base
{
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public abstract class BaseApiController : ApiController
    {
        protected IHttpActionResult GetHttpActionResult<T>(Result<T, Error> result)
        {
            return result.IsSuccess ? Ok(result.Value) : GetErrorHttpActionResult(result);
        }

        protected IHttpActionResult GetHttpActionResultForDelete(Result<Error> result)
        {
            return result.IsSuccess ? new StatusCodeResult(HttpStatusCode.NoContent, this) : GetErrorHttpActionResult(result);
        }

        private IHttpActionResult GetErrorHttpActionResult(IResult<Error> result)
        {
            return result.Error.ErrorType == ErrorType.BadRequest ? (IHttpActionResult)BadRequest(result.Error.Message) : NotFound();
        }
    }
}