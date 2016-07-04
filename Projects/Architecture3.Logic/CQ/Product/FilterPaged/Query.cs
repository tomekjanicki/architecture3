namespace Architecture3.Logic.CQ.Product.FilterPaged
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Query : ValueObject<Query>, IRequest<Paged<Product>>
    {
        private Query(string name, string code, OrderByTopSkip orderByTopSkip)
        {
            Name = name;
            Code = code;
            OrderByTopSkip = orderByTopSkip;
        }

        public OrderByTopSkip OrderByTopSkip { get; }

        public string Name { get; }

        public string Code { get; }

        public static Result<Query, string> Create(string orderBy, int skip, int pageSize, string filter)
        {
            // todo filter parser
            // todo orderBy parser
            var r1 = OrderByTopSkip.Create(string.Empty, skip, pageSize);
            return r1.IsFailure ? Result<Query, string>.Fail(r1.Error) : Result<Query, string>.Ok(new Query(string.Empty, string.Empty, r1.Value));
        }

        protected override bool EqualsCore(Query other)
        {
            return OrderByTopSkip == other.OrderByTopSkip && Name == other.Name && Code == other.Code;
        }

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = hash * 7 + OrderByTopSkip.GetHashCode();
            hash = hash * 7 + Name.GetHashCode();
            hash = hash * 7 + Code.GetHashCode();
            return hash;
        }
    }
}
