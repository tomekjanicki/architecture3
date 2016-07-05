namespace Architecture3.Logic.Facades
{
    using Architecture3.Common;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Post;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Architecture3.WebApi.Dtos.Product.Post;

    public sealed class ProductsPostFacade
    {
        private readonly IMediator _mediator;

        public ProductsPostFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Result<int, Error> Post(Product product)
        {
            var commandResult = Command.Create(product.Name.ToEmptyString(), product.Code.ToEmptyString(), product.Price);

            if (commandResult.IsFailure)
            {
                return ((NonEmptyString)commandResult.Error).ToBadRequest<int>();
            }

            var result = _mediator.Send(commandResult.Value);

            return result;
        }
    }
}