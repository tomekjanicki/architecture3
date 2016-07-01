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
        private readonly IResultMapper _filterPagedResultMapper;
        private readonly Logic.Product.Get.Interfaces.IResultMapper _getResultMapper;

        public ProductsController(IMediator mediator, IResultMapper filterPagedResultMapper, Logic.Product.Get.Interfaces.IResultMapper getResultMapper)
        {
            _mediator = mediator;
            _filterPagedResultMapper = filterPagedResultMapper;
            _getResultMapper = getResultMapper;
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

            var data = _filterPagedResultMapper.Map(result);

            return Ok(data);
        }

        [SwaggerResponse(HttpStatusCode.OK, null, typeof(Dtos.Product.Get.Product))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult Get(int id)
        {
            var queryResult = Logic.Product.Get.Query.Create(id);

            if (queryResult.IsFailure)
            {
                return BadRequest(queryResult.Error);
            }

            var result = _mediator.Send(queryResult.Value);

            var data = _getResultMapper.Map(result);

            return Ok(data);
        }
    }
}
