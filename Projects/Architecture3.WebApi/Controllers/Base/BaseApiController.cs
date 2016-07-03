namespace Architecture3.WebApi.Controllers.Base
{
    using System.Web.Http;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public abstract class BaseApiController : ApiController
    {
        protected IHttpActionResult GetHttpActionResult<T>(Result<T, Error> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            if (result.Error.ErrorType == ErrorType.BadRequest)
            {
                return BadRequest(result.Error.Message);
            }

            return NotFound();
        }
    }
}