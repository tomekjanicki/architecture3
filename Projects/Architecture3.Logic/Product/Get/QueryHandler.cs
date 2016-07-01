namespace Architecture3.Logic.Product.Get
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Product.Get.Interfaces;

    public class QueryHandler : IRequestHandler<Query, Product>
    {
        private readonly IRepository _repository;

        public QueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Product Handle(Query message)
        {
            return _repository.Fetch(message);
        }
    }
}
