namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Post;
    using Architecture3.Logic.Facades.Shared;
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
            var commandResult = Command.Create(product.Name, product.Code, product.Price);

            if (commandResult.IsFailure)
            {
                return Result<int, Error>.Fail(Error.CreateBadRequest(commandResult.Error));
            }

            var result = _mediator.Send(commandResult.Value);

            return result;
        }
    }
}