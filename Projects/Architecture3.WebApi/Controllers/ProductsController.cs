namespace Architecture3.WebApi.Controllers
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.Logic.Facades;
    using Architecture3.WebApi.Controllers.Base;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Dtos.Product.Put;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class ProductsController : BaseApiController
    {
        private readonly FilterPagedFacade _filterPagedFacade;
        private readonly ProductsGetFacade _productsGetFacade;
        private readonly ProductsDeleteFacade _productsDeleteFacade;
        private readonly ProductsPutFacade _productsPutFacade;
        private readonly ProductsPostFacade _productsPostFacade;

        public ProductsController(FilterPagedFacade filterPagedFacade, ProductsGetFacade productsGetFacade, ProductsDeleteFacade productsDeleteFacade, ProductsPutFacade productsPutFacade, ProductsPostFacade productsPostFacade)
        {
            _filterPagedFacade = filterPagedFacade;
            _productsGetFacade = productsGetFacade;
            _productsDeleteFacade = productsDeleteFacade;
            _productsPutFacade = productsPutFacade;
            _productsPostFacade = productsPostFacade;
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Paged<Dtos.Product.FilterPaged.Product>))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [HttpGet]
        public IHttpActionResult FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var result = _filterPagedFacade.FilterPaged(skip, top, filter, orderBy);

            return GetHttpActionResult(result);
        }

        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Put(int id, Product product)
        {
            var result = _productsPutFacade.Put(id, product);

            return GetHttpActionResultForPut(result);
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult Post(WebApi.Dtos.Product.Post.Product product)
        {
            var result = _productsPostFacade.Post(product);

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

        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Delete(int id, string version)
        {
            var result = _productsDeleteFacade.Delete(id, version);

            return GetHttpActionResultForDelete(result);
        }
    }
}
