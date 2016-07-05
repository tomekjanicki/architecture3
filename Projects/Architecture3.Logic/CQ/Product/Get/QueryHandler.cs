namespace Architecture3.Logic.CQ.Product.Get
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Get.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class QueryHandler : IRequestHandler<Query, Result<Product, Error>>
    {
        private readonly IRepository _repository;

        public QueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Result<Product, Error> Handle(Query message)
        {
            var data = _repository.Get(message);
            if (data.HasNoValue)
            {
                return ErrorResultExtensions.ToNotFound<Product>();
            }

            return Result<Product, Error>.Ok(data.Value);
        }
    }
}
