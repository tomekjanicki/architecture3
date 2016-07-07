namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.FilterPaged;
    using Architecture3.Logic.Facades.Base;
    using Architecture3.Logic.Facades.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types.FunctionalExtensions;
    using Architecture3.WebApi.Dtos;
    using AutoMapper;

    public sealed class ProductsFilterPagedFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductsFilterPagedCriteriaProvider _productsFilterPagedCriteriaProvider;

        public ProductsFilterPagedFacade(IMediator mediator, IMapper mapper, IProductsFilterPagedCriteriaProvider productsFilterPagedCriteriaProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productsFilterPagedCriteriaProvider = productsFilterPagedCriteriaProvider;
        }

        public Result<Paged<WebApi.Dtos.Product.FilterPaged.Product>, Error> FilterPaged(int skip, int top, string filter, string orderBy)
        {
            var criteria = _productsFilterPagedCriteriaProvider.Get(filter, orderBy);

            var queryResult = Query.Create(criteria.OrderBy, skip, top, criteria.Name, criteria.Code);

            return Helper.GetItems<Paged<WebApi.Dtos.Product.FilterPaged.Product>, Query, Common.ValueObjects.Paged<Product>>(_mediator, _mapper, queryResult);
        }
    }
}