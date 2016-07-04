namespace Architecture3.Logic.CQ.Product.Delete
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
            var id = message.IdVersion.Id;
            var version = message.IdVersion.Version;

            var exists = _repository.ExistsById(id);

            if (!exists)
            {
                return Result<Error>.Fail(Error.CreateNotFound());
            }

            var versionFromRepository = _repository.GetRowVersionById(id);

            if (versionFromRepository != version)
            {
                return Result<Error>.Fail(Error.CreateBadRequest("Versions are not equal"));
            }

            var canBeDeleted = _repository.CanBeDeleted(id);

            if (!canBeDeleted)
            {
                return Result<Error>.Fail(Error.CreateBadRequest("Can't delete because ther are rows dependent on that item"));
            }

            _repository.Delete(id);

            return Result<Error>.Ok();
        }
    }
}
