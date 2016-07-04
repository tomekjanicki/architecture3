namespace Architecture3.Logic.CQ.Product.Put
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Put.Interfaces;
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

            if (versionFromRepository.HasValue)
            {
                if (versionFromRepository.Value != version)
                {
                    return Result<Error>.Fail(Error.CreatePreconditionFailed());
                }
            }
            else
            {
                return Result<Error>.Fail(Error.CreateBadRequest("GetRowVersionById returned no rows"));
            }

            _repository.Update(message);

            return Result<Error>.Ok();
        }
    }
}
