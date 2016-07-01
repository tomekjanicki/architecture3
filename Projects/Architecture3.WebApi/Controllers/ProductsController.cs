namespace Architecture3.WebApi.Controllers
{
    using System.Net;
    using System.Web.Http;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Product.FilterPaged;
    using Architecture3.Logic.Product.FilterPaged.Interfaces;
    using Architecture3.WebApi.Dtos;
    using Swashbuckle.Swagger.Annotations;

    [SwaggerResponseRemoveDefaults]
    public class ProductsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IResultMapper _resultMapper;

        public ProductsController(IMediator mediator, IResultMapper resultMapper)
        {
            _mediator = mediator;
            _resultMapper = resultMapper;
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Paged<Dtos.Product.FilterPaged.Product>))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [HttpGet]
        public IHttpActionResult FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var queryResult = Query.Create(orderBy, skip, top, filter);

            if (queryResult.IsFailure)
            {
                return BadRequest(queryResult.Error);
            }

            var result = _mediator.Send(queryResult.Value);

            var data = _resultMapper.Map(result);

            return Ok(data);
        }
    }
}
