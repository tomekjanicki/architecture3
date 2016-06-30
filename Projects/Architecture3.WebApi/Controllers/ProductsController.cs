namespace Architecture3.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Dtos.Product.FilterPaged;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public class ProductsController : ApiController
    {
        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Paged<Product>))]
        [HttpGet]
        public IHttpActionResult FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var data = new Paged<Product>(10, new List<Product> { new Product() });
            return Ok(data);
        }
    }
}
