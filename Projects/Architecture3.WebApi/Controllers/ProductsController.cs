namespace Architecture3.WebApi.Controllers
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Facades;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public class ProductsController : ApiController
    {
        private readonly FilterPagedFacade _filterPagedFacade;
        private readonly ProductsGetFacade _productsGetFacade;

        public ProductsController(FilterPagedFacade filterPagedFacade, ProductsGetFacade productsGetFacade)
        {
            _filterPagedFacade = filterPagedFacade;
            _productsGetFacade = productsGetFacade;
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Paged<Dtos.Product.FilterPaged.Product>))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [HttpGet]
        public IHttpActionResult FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var result = _filterPagedFacade.FilterPaged(skip, top, filter, orderBy);

            if (result.Item1 == ResponseType.BadRequest)
            {
                return BadRequest(result.Item2);
            }

            return Ok(result.Item3);
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Dtos.Product.Get.Product))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Get(int id)
        {
            var result = _productsGetFacade.Get(id);

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
