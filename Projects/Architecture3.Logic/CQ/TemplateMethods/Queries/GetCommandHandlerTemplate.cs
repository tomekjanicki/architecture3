namespace Architecture3.Logic.CQ.TemplateMethods.Queries
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Queries.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public class GetCommandHandlerTemplate<TQuery, TGetRepository, TItem> : IRequestHandler<TQuery, IResult<TItem, Error>>
        where TQuery : IId, IRequest<IResult<TItem, Error>>
        where TGetRepository : class, IGetRepository<TItem>
        where TItem : class
    {
        protected GetCommandHandlerTemplate(TGetRepository getRepository)
        {
            GetRepository = getRepository;
        }

        protected TGetRepository GetRepository { get; }

        public IResult<TItem, Error> Handle(TQuery message)
        {
            var data = GetRepository.Get(message.Id);

            return data.HasNoValue ? ErrorResultExtensions.ToNotFound<TItem>() : Result<TItem, Error>.Ok(data.Value);
        }
    }
}
