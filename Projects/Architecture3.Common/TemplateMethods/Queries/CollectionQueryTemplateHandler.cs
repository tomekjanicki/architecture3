namespace Architecture3.Common.TemplateMethods.Queries
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.SharedStructs.RequestParams;
    using Architecture3.Common.SharedStructs.ResponseParams;
    using Architecture3.Common.TemplateMethods.Queries.Interfaces;
    using Architecture3.Common.Tools;
    using global::FluentValidation;

    public abstract class CollectionQueryTemplateHandler<TQuery, TItem, TCollectionRepository> : IRequestHandler<TQuery, CollectionResult<TItem>>
        where TQuery : Sort<TItem>
        where TCollectionRepository : class, ICollectionRepository<TItem, TQuery>
    {
        private readonly IValidator<TQuery> _validator;

        protected CollectionQueryTemplateHandler(IValidator<TQuery> validator, TCollectionRepository collectionRepository)
        {
            Guard.NotNull(collectionRepository, nameof(collectionRepository));
            CollectionRepository = collectionRepository;
            _validator = validator;
        }

        protected TCollectionRepository CollectionRepository { get; }

        public CollectionResult<TItem> Handle(TQuery message)
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

        protected virtual CollectionResult<TItem> ExecuteFetch(TQuery message)
        {
            return CollectionRepository.Fetch(message);
        }
    }
}