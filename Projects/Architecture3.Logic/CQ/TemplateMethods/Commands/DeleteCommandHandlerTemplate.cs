namespace Architecture3.Logic.CQ.TemplateMethods.Commands
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public abstract class DeleteCommandHandlerTemplate<TCommand, TDeleteRepository> : IRequestHandler<TCommand, Result<Error>>
        where TCommand : IIdVersion, IRequest<Result<Error>>
        where TDeleteRepository : class, IDeleteRepository
    {
        protected DeleteCommandHandlerTemplate(TDeleteRepository deleteRepository)
        {
            DeleteRepository = deleteRepository;
        }

        protected TDeleteRepository DeleteRepository { get; }

        public Result<Error> Handle(TCommand message)
        {
            var id = message.IdVersion.Id;
            var version = message.IdVersion.Version;

            var exists = DeleteRepository.ExistsById(id);

            if (!exists)
            {
                return Result<Error>.Fail(Error.CreateNotFound());
            }

            var versionFromRepository = DeleteRepository.GetRowVersionById(id);

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

            var result = BeforeDelete(message);

            if (result.IsFailure)
            {
                return Result<Error>.Fail(Error.CreateBadRequest(result.Error.Message));
            }

            DeleteRepository.Delete(id);

            return Result<Error>.Ok();
        }

        protected virtual Result<Error> BeforeDelete(TCommand message)
        {
            return Result<Error>.Ok();
        }
    }
}
