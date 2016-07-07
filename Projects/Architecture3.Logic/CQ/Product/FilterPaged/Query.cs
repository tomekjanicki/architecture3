﻿namespace Architecture3.Logic.CQ.Product.FilterPaged
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Types;
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

        public static Result<Query, NonEmptyString> Create(string orderBy, int skip, int top, string name, string code)
        {
            var result = OrderByTopSkip.Create(orderBy, skip, top, (NonEmptyString)nameof(TopSkip.Top), (NonEmptyString)nameof(TopSkip.Skip));
            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new Query(name, code, result.Value));
        }

        protected override bool EqualsCore(Query other)
        {
            return OrderByTopSkip == other.OrderByTopSkip && Name == other.Name && Code == other.Code;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { OrderByTopSkip, Name, Code });
        }
    }
}
