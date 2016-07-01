namespace Architecture3.WebApi.Controllers.Base
{
    using System;
    using System.Web.Http;
    using Architecture3.Logic.Facades;

    public abstract class BaseApiController : ApiController
    {
        protected IHttpActionResult GetHttpActionResult<T>(Tuple<ResponseType, string, T> result)
        {
            if (result.Item1 == ResponseType.BadRequest)
            {
                return BadRequest(result.Item2);
            }

            if (result.Item1 == ResponseType.NotFound)
            {
                return NotFound();
            }

            return Ok(result.Item3);
        }
    }
}