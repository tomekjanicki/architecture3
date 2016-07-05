namespace Architecture3.Logic.CQ.Product.Get
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Queries.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Query : ValueObject<Query>, IRequest<Result<Product, Error>>, IId
    {
        private Query(NonNegativeInt id)
        {
            Id = id;
        }

        public NonNegativeInt Id { get; }

        public static Result<Query, string> Create(int id)
        {
            var result = NonNegativeInt.Create(id, (NonEmptyString)nameof(Id));
            return result.IsFailure ? GetFailResult((NonEmptyString)result.Error) : GetOkResult(new Query(result.Value));
        }

        protected override bool EqualsCore(Query other)
        {
            return Id == other.Id;
        }

        protected override int GetHashCodeCore()
        {
            return Id.GetHashCode();
        }
    }
}
