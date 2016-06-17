namespace Architecture3.WebApi.Controllers
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.SharedStructs.ResponseParams;
    using Architecture3.Logic.Product.FindPagedCollection;
    using Architecture3.WebApi.Dtos.Product.FindPagedCollection;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public class ProductsController : ApiController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(PagedCollectionResult<ProductItem>))]
        [HttpGet]
        public IHttpActionResult FindPaged(int pageSize, int skip, string code = null, string name = null, string sort = null)
        {
            var data = _mediator.Send(new Query { PageSize = pageSize, Skip = skip, SortExp = sort, Code = code, Name = name });

            return Ok(data);
        }
    }
}
