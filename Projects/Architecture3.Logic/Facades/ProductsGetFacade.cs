namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Get;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;
    using AutoMapper;

    public class ProductsGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsGetFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public Result<CQ.Product.Get.Product, Error> Get(int id)
        {
            var queryResult = Query.Create(id);

            if (queryResult.IsFailure)
            {
                return Result<CQ.Product.Get.Product, Error>.Fail(Error.CreateBadRequest(queryResult.Error));
            }

            var result = _mediator.Send(queryResult.Value);

            if (result.HasNoValue)
            {
                return Result<CQ.Product.Get.Product, Error>.Fail(Error.CreateNotFound());
            }

            var data = _mapper.Map<CQ.Product.Get.Product>(result.Value);

            return Result<CQ.Product.Get.Product, Error>.Ok(data);
        }
    }
}