namespace Architecture3.Common.ValueObjects
{
    using Architecture3.Types.FunctionalExtensions;

    public sealed class OrderByTopSkip : ValueObject<OrderByTopSkip>
    {
        private OrderByTopSkip(string orderBy, TopSkip topSkip)
        {
            TopSkip = topSkip;
            OrderBy = orderBy;
        }

        public string OrderBy { get; }

        public TopSkip TopSkip { get; }

        public static Result<OrderByTopSkip, string> Create(string orderBy, int skip, int top, string skipField, string topField, string orderByField)
        {
            var topSkipResult = TopSkip.Create(skip, top, skipField, topField);

            return topSkipResult.IsFailure ? GetFailResult(topSkipResult.Error) : GetOkResult(new OrderByTopSkip(orderBy, topSkipResult.Value));
        }

        protected override bool EqualsCore(OrderByTopSkip other)
        {
            return TopSkip == other.TopSkip && OrderBy == other.OrderBy;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { TopSkip, OrderBy });
        }
    }
}