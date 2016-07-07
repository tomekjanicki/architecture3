namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.FilterPaged;
    using Architecture3.Logic.Facades.Base;
    using Architecture3.Logic.Shared;
    using Architecture3.Types.FunctionalExtensions;
    using Architecture3.WebApi.Dtos;
    using AutoMapper;

    public sealed class ProductsFilterPagedFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsFilterPagedFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public Result<Paged<WebApi.Dtos.Product.FilterPaged.Product>, Error> FilterPaged(int skip, int top, string filter, string orderBy)
        {
            var queryResult = Query.Create(orderBy, skip, top, filter);

            return Helper.GetItems<Paged<WebApi.Dtos.Product.FilterPaged.Product>, Query, Common.ValueObjects.Paged<Product>>(_mediator, _mapper, queryResult);
        }
    }
}