namespace Architecture3.Web.Controllers
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.Logic.Facades;
    using Architecture3.Web.Controllers.Base;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class VersionController : BaseApiController
    {
        private readonly VersionGetFacade _versionGetFacade;

        public VersionController(VersionGetFacade versionGetFacade)
        {
            _versionGetFacade = versionGetFacade;
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(string))]
        public IHttpActionResult Get()
        {
            var result = _versionGetFacade.Get(GetType().Assembly);

            return GetHttpActionResult(result);
        }
    }
}