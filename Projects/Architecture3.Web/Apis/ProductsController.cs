namespace Architecture3.Web.Apis
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.Logic.Facades.Apis;
    using Architecture3.Types;
    using Architecture3.Web.Dtos;
    using Architecture3.Web.Dtos.Apis.Product.FilterPaged;
    using Architecture3.Web.Infrastructure;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public sealed class ProductsController : BaseWebApiController
    {
        private readonly ProductsFilterPagedFacade _productsFilterPagedFacade;
        private readonly ProductsGetFacade _productsGetFacade;
        private readonly ProductsDeleteFacade _productsDeleteFacade;
        private readonly ProductsPutFacade _productsPutFacade;
        private readonly ProductsPostFacade _productsPostFacade;

        public ProductsController(ProductsFilterPagedFacade productsFilterPagedFacade, ProductsGetFacade productsGetFacade, ProductsDeleteFacade productsDeleteFacade, ProductsPutFacade productsPutFacade, ProductsPostFacade productsPostFacade)
        {
            _productsFilterPagedFacade = productsFilterPagedFacade;
            _productsGetFacade = productsGetFacade;
            _productsDeleteFacade = productsDeleteFacade;
            _productsPutFacade = productsPutFacade;
            _productsPostFacade = productsPostFacade;
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Paged<Product>))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [HttpGet]
        public IHttpActionResult FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var result = _productsFilterPagedFacade.FilterPaged(skip, top, filter.ToEmptyString(), orderBy.ToEmptyString());

            return GetHttpActionResult(result);
        }

        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Put(int id, Dtos.Apis.Product.Put.Product product)
        {
            var result = _productsPutFacade.Put(id, product);

            return GetHttpActionResultForPut(result);
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult Post(Dtos.Apis.Product.Post.Product product)
        {
            var result = _productsPostFacade.Post(product);

            return GetHttpActionResult(result);
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Dtos.Apis.Product.Get.Product))]
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
            var result = _productsDeleteFacade.Delete(id, version.ToEmptyString());

            return GetHttpActionResultForDelete(result);
        }
    }
}
