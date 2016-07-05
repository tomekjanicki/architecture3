namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Get;
    using Architecture3.Logic.Shared;
    using Architecture3.Types.FunctionalExtensions;
    using AutoMapper;

    public sealed class ProductsGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsGetFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public Result<WebApi.Dtos.Product.Get.Product, Error> Get(int id)
        {
            var queryResult = Query.Create(id);

            if (queryResult.IsFailure)
            {
                return queryResult.Error.ToBadRequest<WebApi.Dtos.Product.Get.Product>();
            }

            var result = _mediator.Send(queryResult.Value);

            if (result.IsFailure)
            {
                return Result<WebApi.Dtos.Product.Get.Product, Error>.Fail(result.Error);
            }

            var data = _mapper.Map<WebApi.Dtos.Product.Get.Product>(result.Value);

            return Result<WebApi.Dtos.Product.Get.Product, Error>.Ok(data);
        }
    }
}