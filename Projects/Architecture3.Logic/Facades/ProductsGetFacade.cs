namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Product.Get;
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

        public R<Product, Error?> Get(int id)
        {
            var queryResult = Query.Create(id);

            if (queryResult.IsFailure)
            {
                return R<Product, Error?>.Fail(Error.CreateBadRequest(queryResult.Error));
            }

            var result = _mediator.Send(queryResult.Value);

            if (result.HasNoValue)
            {
                return R<Product, Error?>.Fail(Error.CreateNotFound());
            }

            var data = _mapper.Map<Product>(result.Value);

            return R<Product, Error?>.Ok(data);
        }
    }
}