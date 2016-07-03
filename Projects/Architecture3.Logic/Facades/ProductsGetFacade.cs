namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Logic.Product.Get;
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

        public Result<Product, Error> Get(int id)
        {
            var queryResult = Query.Create(id);

            if (queryResult.IsFailure)
            {
                return Result<Product, Error>.Fail(Error.CreateBadRequest(queryResult.Error));
            }

            var result = _mediator.Send(queryResult.Value);

            if (result.HasNoValue)
            {
                return Result<Product, Error>.Fail(Error.CreateNotFound());
            }

            var data = _mapper.Map<Product>(result.Value);

            return Result<Product, Error>.Ok(data);
        }
    }
}