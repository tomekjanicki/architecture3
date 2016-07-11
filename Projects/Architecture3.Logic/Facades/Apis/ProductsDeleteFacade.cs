namespace Architecture3.Logic.Facades.Apis
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Apis.Product.Delete;
    using Architecture3.Logic.Facades.Base;
    using Architecture3.Logic.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class ProductsDeleteFacade
    {
        private readonly IMediator _mediator;

        public ProductsDeleteFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Delete(int id, string version)
        {
            var commandResult = Command.Create(id, version);

            return Helper.Delete(_mediator, commandResult);
        }
    }
}
