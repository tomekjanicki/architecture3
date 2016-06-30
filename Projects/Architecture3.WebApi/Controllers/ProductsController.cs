namespace Architecture3.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Dtos.Product.FindPagedCollection;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public class ProductsController : ApiController
    {
        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Paged<ProductItem>))]
        [HttpGet]
        public IHttpActionResult FindPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var data = new Paged<ProductItem>(10, new List<ProductItem> { new ProductItem() });
            return Ok(data);
        }
    }
}
