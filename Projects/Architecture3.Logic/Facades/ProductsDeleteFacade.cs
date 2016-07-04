﻿namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Delete;
    using Architecture3.Logic.Facades.Shared;
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
                return Result<Error>.Fail(Error.CreateBadRequest(commandResult.Error));
            }

            var result = _mediator.Send(commandResult.Value);

            if (result.IsFailure)
            {
                return Result<Error>.Fail(result.Error.ErrorType == ErrorType.BadRequest ? Error.CreateBadRequest(result.Error.Message) : Error.CreateNotFound());
            }

            return Result<Error>.Ok();
        }
    }
}