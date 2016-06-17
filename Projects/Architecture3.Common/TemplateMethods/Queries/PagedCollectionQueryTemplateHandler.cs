namespace Architecture3.Common.TemplateMethods.Queries
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.SharedStructs.RequestParams;
    using Architecture3.Common.SharedStructs.ResponseParams;
    using Architecture3.Common.TemplateMethods.Queries.Interfaces;
    using Architecture3.Common.Tools;
    using global::FluentValidation;

    public abstract class PagedCollectionQueryTemplateHandler<TQuery, TItem, TPagedCollectionRepository> : IRequestHandler<TQuery, PagedCollectionResult<TItem>>
        where TQuery : SortPageSizeSkip<TItem>
        where TPagedCollectionRepository : class, IPagedCollectionRepository<TItem, TQuery>
    {
        private readonly IValidator<TQuery> _validator;

        protected PagedCollectionQueryTemplateHandler(IValidator<TQuery> validator, TPagedCollectionRepository pagedCollectionRepository)
        {
            Guard.NotNull(pagedCollectionRepository, nameof(pagedCollectionRepository));
            PagedCollectionRepository = pagedCollectionRepository;
            _validator = validator;
        }

        protected TPagedCollectionRepository PagedCollectionRepository { get; }

        public PagedCollectionResult<TItem> Handle(TQuery message)
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

        protected virtual PagedCollectionResult<TItem> ExecuteFetch(TQuery message)
        {
            return PagedCollectionRepository.Fetch(message);
        }
    }
}
