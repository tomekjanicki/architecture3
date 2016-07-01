namespace Architecture3.Logic.Product.FilterPaged
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using CSharpFunctionalExtensions;

    public class Query : ValueObject<Query>, IRequest<Paged<Product>>
    {
        private Query(string name, string code, SortPageSizeSkip sortPageSizeSkip)
        {
            Name = name;
            Code = code;
            SortPageSizeSkip = sortPageSizeSkip;
        }

        public SortPageSizeSkip SortPageSizeSkip { get; }

        public string Name { get; }

        public string Code { get; }

        public static Result<Query> Create(string orderBy, int skip, int pageSize, string filter)
        {
            // todo filter parser
            // todo orderBy parser
            var r1 = SortPageSizeSkip.Create(string.Empty, skip, pageSize);
            return r1.IsFailure ? Result.Fail<Query>(r1.Error) : Result.Ok(new Query(string.Empty, string.Empty, r1.Value));
        }

        protected override bool EqualsCore(Query other)
        {
            return SortPageSizeSkip == other.SortPageSizeSkip && Name == other.Name && Code == other.Code;
        }

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = hash * 7 + SortPageSizeSkip.GetHashCode();
            hash = hash * 7 + Name.GetHashCode();
            hash = hash * 7 + Code.GetHashCode();
            return hash;
        }
    }
}
