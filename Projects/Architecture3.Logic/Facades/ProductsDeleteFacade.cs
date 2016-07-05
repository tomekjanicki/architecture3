namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Delete;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class ProductsDeleteFacade
    {
        private readonly IMediator _mediator;

        public ProductsDeleteFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Result<Error> Delete(int id, string version)
        {
            var commandResult = Command.Create(id, version);

            if (commandResult.IsFailure)
            {
                return ((NonEmptyString)commandResult.Error).ToBadRequest();
            }

            var result = _mediator.Send(commandResult.Value);

            return result;
        }
    }
}
