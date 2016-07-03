namespace Architecture3.Logic.CQ.Product.FilterPaged
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Logic.CQ.Product.FilterPaged.Interfaces;

    public class QueryHandler : IRequestHandler<Query, Paged<Product>>
    {
        private readonly IRepository _repository;

        public QueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Paged<Product> Handle(Query message)
        {
            return _repository.Fetch(message);
        }
    }
}
