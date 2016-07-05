namespace Architecture3.Logic.CQ.TemplateMethods.Commands
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public abstract class UpdateCommandHandlerTemplate<TCommand, TUpdateRepository> : IRequestHandler<TCommand, Result<Error>>
        where TCommand : IIdVersion, IRequest<Result<Error>>
        where TUpdateRepository : class, IUpdateRepository<TCommand>
    {
        protected UpdateCommandHandlerTemplate(TUpdateRepository updateRepository)
        {
            UpdateRepository = updateRepository;
        }

        protected TUpdateRepository UpdateRepository { get; }

        public Result<Error> Handle(TCommand message)
        {
            var id = message.IdVersion.Id;
            var version = message.IdVersion.Version;

            var exists = UpdateRepository.ExistsById(id);

            if (!exists)
            {
                return ErrorResultExtensions.ToNotFound();
            }

            var versionFromRepository = UpdateRepository.GetRowVersionById(id);

            if (versionFromRepository.HasValue)
            {
                if (versionFromRepository.Value != version)
                {
                    return ErrorResultExtensions.ToPreconditionFailed();
                }
            }
            else
            {
                return ((NonEmptyString)"GetRowVersionById returned no rows").ToBadRequest();
            }

            var result = BeforeUpdate(message);

            if (result.IsFailure)
            {
                return result.Error.ToBadRequest();
            }

            UpdateRepository.Update(message);

            return Result<Error>.Ok();
        }

        protected virtual Result<NonEmptyString> BeforeUpdate(TCommand message)
        {
            return Result<NonEmptyString>.Ok();
        }
    }
}
