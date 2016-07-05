namespace Architecture3.Logic.CQ.Product.Get
{
    using Architecture3.Logic.CQ.Product.Get.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Queries;

    public sealed class QueryHandler : GetCommandHandlerTemplate<Query, IRepository, Product>
    {
        public QueryHandler(IRepository repository)
            : base(repository)
        {
        }
    }
}
