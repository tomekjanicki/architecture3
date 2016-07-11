namespace Architecture3.Web.Apis
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.Logic.Facades.Apis;
    using Architecture3.Web.Infrastructure;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class VersionController : BaseWebApiController
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