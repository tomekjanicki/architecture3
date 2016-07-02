﻿namespace Architecture3.Logic.Product.Get
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public class Query : ValueObject<Query>, IRequest<Maybe<Product>>
    {
        private Query(NonNegativeInt id)
        {
            Id = id;
        }

        public NonNegativeInt Id { get; }

        public static ResultX<Query, string> Create(int id)
        {
            var r1 = NonNegativeInt.Create(id);
            return r1.IsFailure ? ResultX<Query, string>.Fail(r1.Error) : ResultX<Query, string>.Ok(new Query(r1.Value));
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
