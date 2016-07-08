namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Put;
    using Architecture3.Logic.Facades.Base;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Architecture3.Web.Dtos.Product.Put;

    public sealed class ProductsPutFacade
    {
        private readonly IMediator _mediator;

        public ProductsPutFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Put(int id, Product product)
        {
            var commandResult = Command.Create(id, product.Version.ToEmptyString(), product.Price, product.Name.ToEmptyString());

            return Helper.Put(_mediator, commandResult);
        }
    }
}