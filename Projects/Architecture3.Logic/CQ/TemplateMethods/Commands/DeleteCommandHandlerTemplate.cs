﻿namespace Architecture3.Logic.CQ.TemplateMethods.Commands
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
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
                return ErrorResultExtensions.ToNotFound();
            }

            var versionFromRepository = DeleteRepository.GetRowVersionById(id);

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

            var result = BeforeDelete(message);

            if (result.IsFailure)
            {
                return result.Error.ToBadRequest();
            }

            DeleteRepository.Delete(id);

            return Result<Error>.Ok();
        }

        protected virtual Result<NonEmptyString> BeforeDelete(TCommand message)
        {
            return Result<NonEmptyString>.Ok();
        }
    }
}
