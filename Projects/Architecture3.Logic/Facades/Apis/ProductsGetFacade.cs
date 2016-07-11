namespace Architecture3.Logic.Facades.Apis
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Apis.Product.Get;
    using Architecture3.Logic.Facades.Base;
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

        public IResult<Web.Dtos.Apis.Product.Get.Product, Error> Get(int id)
        {
            var queryResult = Query.Create(id);

            return Helper.GetItem<Web.Dtos.Apis.Product.Get.Product, Query, Product>(_mediator, _mapper, queryResult);
        }
    }
}