namespace Architecture3.Logic.Product.FindPagedCollection
{
    using Architecture3.Common.TemplateMethods.Queries;
    using Architecture3.Common.TemplateMethods.Queries.Interfaces;
    using Architecture3.WebApi.Dtos.Product.FindPagedCollection;
    using FluentValidation;

    public class QueryHandler : PagedCollectionQueryTemplateHandler<Query, ProductItem, IPagedCollectionRepository<ProductItem, Query>>
    {
        public QueryHandler(IPagedCollectionRepository<ProductItem, Query> pagedCollectionRepository, IValidator<Query> validator)
            : base(validator, pagedCollectionRepository)
        {
        }
    }
}
