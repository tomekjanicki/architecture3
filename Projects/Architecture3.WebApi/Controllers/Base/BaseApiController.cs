namespace Architecture3.WebApi.Controllers.Base
{
    using System.Web.Http;
    using Architecture3.Logic.Facades;

    public abstract class BaseApiController : ApiController
    {
        protected IHttpActionResult GetHttpActionResult<T>(R<T, Error?> result)
        {
            if (!result.ErrorResult.HasValue)
            {
                return Ok(result.Value);
            }

            if (result.ErrorResult.Value.ErrorType == ErrorType.BadRequest)
            {
                return BadRequest(result.ErrorResult.Value.Message);
            }

            return NotFound();
        }
    }
}