namespace Architecture3.WebApi.Controllers
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.Common.SharedStructs.ResponseParams;
    using Architecture3.WebApi.Dtos.Product;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public class ProductsController : ApiController
    {
        [SwaggerResponse(HttpStatusCode.OK, null, typeof(PagedCollectionResult<ProductItem>))]
        [HttpGet]
        public IHttpActionResult FindPaged()
        {
            return null;
        }
    }
}
