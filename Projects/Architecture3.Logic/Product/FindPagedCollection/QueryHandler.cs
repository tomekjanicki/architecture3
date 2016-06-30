namespace Architecture3.Logic.Product.FindPagedCollection
{
    using Architecture3.Common.TemplateMethods.Queries;
    using Architecture3.Common.TemplateMethods.Queries.Interfaces;
    using FluentValidation;

    public class QueryHandler : PagedCollectionQueryTemplateHandler<Query, WebApi.Dtos.Product.FilterPaged.Product, IPagedCollectionRepository<WebApi.Dtos.Product.FilterPaged.Product, Query>>
    {
        public QueryHandler(IPagedCollectionRepository<WebApi.Dtos.Product.FilterPaged.Product, Query> pagedCollectionRepository, IValidator<Query> validator)
            : base(validator, pagedCollectionRepository)
        {
        }
    }
}
