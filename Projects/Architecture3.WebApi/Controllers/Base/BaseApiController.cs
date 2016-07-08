namespace Architecture3.WebApi.Controllers.Base
{
    using System;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Architecture3.Logic.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public abstract class BaseApiController : ApiController
    {
        protected IHttpActionResult GetHttpActionResult<T>(IResult<T, Error> result)
        {
            return result.IsSuccess ? Ok(result.Value) : GetErrorHttpActionResult(result);
        }

        protected IHttpActionResult GetHttpActionResultForDelete(IResult<Error> result)
        {
            return result.IsSuccess ? new StatusCodeResult(HttpStatusCode.NoContent, this) : GetErrorHttpActionResult(result);
        }

        protected IHttpActionResult GetHttpActionResultForPut(IResult<Error> result)
        {
            return result.IsSuccess ? Ok() : GetErrorHttpActionResult(result);
        }

        private IHttpActionResult GetErrorHttpActionResult(IResult<Error> result)
        {
            switch (result.Error.ErrorType)
            {
                case ErrorType.Generic:
                    return BadRequest(result.Error.Message);
                case ErrorType.PreconditionFailed:
                    return new StatusCodeResult(HttpStatusCode.PreconditionFailed, this);
                case ErrorType.NotFound:
                    return NotFound();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}