namespace Architecture3.WebApi.Controllers
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.Logic.Facades;
    using Architecture3.WebApi.Controllers.Base;
    using Architecture3.WebApi.Dtos;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public class ProductsController : BaseApiController
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

            return GetHttpActionResult(result);
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Dtos.Product.Get.Product))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Get(int id)
        {
            var result = _productsGetFacade.Get(id);

            return GetHttpActionResult(result);
        }
    }
}
