namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Put;
    using Architecture3.Logic.Shared;
    using Architecture3.Types.FunctionalExtensions;
    using Architecture3.WebApi.Dtos.Product.Put;

    public sealed class ProductsPutFacade
    {
        private readonly IMediator _mediator;

        public ProductsPutFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Result<Error> Put(int id, Product product)
        {
            var commandResult = Command.Create(id, product.Version, product.Price, product.Name);

            if (commandResult.IsFailure)
            {
                return Result<Error>.Fail(Error.CreateBadRequest(commandResult.Error));
            }

            var result = _mediator.Send(commandResult.Value);

            return result;
        }
    }
}