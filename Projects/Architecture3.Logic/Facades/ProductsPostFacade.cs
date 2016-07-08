namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Post;
    using Architecture3.Logic.Facades.Base;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Architecture3.Web.Dtos.Product.Post;
    using AutoMapper;

    public sealed class ProductsPostFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsPostFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<int, Error> Post(Product product)
        {
            var commandResult = Command.Create(product.Name.ToEmptyString(), product.Code.ToEmptyString(), product.Price);

            return Helper.Post<int, Command, NonNegativeInt>(_mediator, _mapper, commandResult);
        }
    }
}