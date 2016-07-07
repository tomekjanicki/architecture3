namespace Architecture3.Logic.Facades.Base
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using AutoMapper;

    public static class Helper
    {
        public static Result<TDto, Error> GetItem<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, Result<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<Result<TObject, Error>>
        {
            if (queryResult.IsFailure)
            {
                return queryResult.Error.ToBadRequest<TDto>();
            }

            var result = mediator.Send(queryResult.Value);

            if (result.IsFailure)
            {
                return Result<TDto, Error>.Fail(result.Error);
            }

            var data = mapper.Map<TDto>(result.Value);

            return Result<TDto, Error>.Ok(data);
        }

        public static Result<TDto, Error> GetItemSimple<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, Result<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<TObject>
        {
            return GetItems<TDto, TQuery, TObject>(mediator, mapper, queryResult);
        }

        public static Result<TDto, Error> GetItems<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, Result<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<TObject>
        {
            if (queryResult.IsFailure)
            {
                return queryResult.Error.ToBadRequest<TDto>();
            }

            var result = mediator.Send(queryResult.Value);

            var data = mapper.Map<TDto>(result);

            return Result<TDto, Error>.Ok(data);
        }

        public static Result<Error> Delete<TCommand>(IMediator mediator, Result<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<Result<Error>>
        {
            if (commandResult.IsFailure)
            {
                return commandResult.Error.ToBadRequest();
            }

            var result = mediator.Send(commandResult.Value);

            return result;
        }

        public static Result<Error> Put<TCommand>(IMediator mediator, Result<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<Result<Error>>
        {
            return Delete(mediator, commandResult);
        }

        public static Result<TDto, Error> Post<TDto, TCommand, TObject>(IMediator mediator, IMapper mapper, Result<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<Result<TObject, Error>>
        {
            return GetItem<TDto, TCommand, TObject>(mediator, mapper, commandResult);
        }
    }
}
