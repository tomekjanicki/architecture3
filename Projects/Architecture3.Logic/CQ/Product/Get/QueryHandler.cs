namespace Architecture3.Logic.CQ.Product.Get
{
    using Architecture3.Logic.CQ.TemplateMethods.Queries;
    using Architecture3.Logic.CQ.TemplateMethods.Queries.Interfaces;

    public sealed class QueryHandler : GetCommandHandlerTemplate<Query, IGetRepository<Product>, Product>
    {
        public QueryHandler(IGetRepository<Product> repository)
            : base(repository)
        {
        }
    }
}
