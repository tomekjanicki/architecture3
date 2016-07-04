﻿namespace Architecture3.Logic.CQ.Product.Delete
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Delete.Interfaces;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class CommandHandler : IRequestHandler<Command, Result<Error>>
    {
        private readonly IRepository _repository;

        public CommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Result<Error> Handle(Command message)
        {
            var exists = _repository.ExistsById(message.IdVersion.Id);

            if (!exists)
            {
                return Result<Error>.Fail(Error.CreateNotFound());
            }

            var version = _repository.GetRowVersionById(message.IdVersion.Id);

            if (version != message.IdVersion.Version)
            {
                return Result<Error>.Fail(Error.CreateBadRequest("Versions are not equal"));
            }

            _repository.Delete(message.IdVersion.Id);

            return Result<Error>.Ok();
        }
    }
}
