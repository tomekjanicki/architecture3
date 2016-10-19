namespace Architecture3.Common.ValueObjects
{
    using System.Collections.Generic;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class OrderByTopSkip : ValueObject<OrderByTopSkip>
    {
        private OrderByTopSkip(IReadOnlyCollection<OrderBy> orderBy, TopSkip topSkip)
        {
            TopSkip = topSkip;
            OrderBy = orderBy;
        }

        public IReadOnlyCollection<OrderBy> OrderBy { get; }

        public TopSkip TopSkip { get; }

        public static IResult<OrderByTopSkip, NonEmptyString> Create(IReadOnlyCollection<OrderBy> orderBy, int skip, int top, NonEmptyString skipField, NonEmptyString topField)
        {
            var topSkipResult = TopSkip.Create(skip, top, skipField, topField);

            return topSkipResult.IsFailure ? GetFailResult(topSkipResult.Error) : GetOkResult(new OrderByTopSkip(orderBy, topSkipResult.Value));
        }

        protected override bool EqualsCore(OrderByTopSkip other)
        {
            return TopSkip == other.TopSkip && Equals(OrderBy, other.OrderBy);
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { TopSkip, OrderBy });
        }
    }
}