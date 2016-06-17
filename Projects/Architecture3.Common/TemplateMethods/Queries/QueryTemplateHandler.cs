namespace Architecture3.Common.TemplateMethods.Queries
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.SharedStructs.ResponseParams;
    using Architecture3.Common.TemplateMethods.Queries.Interfaces;
    using Architecture3.Common.Tools;
    using global::FluentValidation;

    public abstract class QueryTemplateHandler<TQuery, TItem, TRepository> : IRequestHandler<TQuery, Result<TItem>>
        where TQuery : IRequest<Result<TItem>>
        where TRepository : class, IRepository<TItem, TQuery>
    {
        private readonly IValidator<TQuery> _validator;

        protected QueryTemplateHandler(IValidator<TQuery> validator, TRepository repository)
        {
            Guard.NotNull(repository, nameof(repository));
            Repository = repository;
            _validator = validator;
        }

        protected TRepository Repository { get; }

        public Result<TItem> Handle(TQuery message)
        {
            ExecuteValidate(message);

            ExecuteBeforeExecuteGet(message);

            return ExecuteFetch(message);
        }

        protected virtual void ExecuteValidate(TQuery message)
        {
            _validator?.ValidateAndThrow(message);
        }

        protected virtual void ExecuteBeforeExecuteGet(TQuery message)
        {
        }

        protected virtual Result<TItem> ExecuteFetch(TQuery message)
        {
            return Repository.Fetch(message);
        }
    }
}