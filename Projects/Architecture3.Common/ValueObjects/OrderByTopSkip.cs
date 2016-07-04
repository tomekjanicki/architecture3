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

        public static Result<OrderByTopSkip, string> Create(string sort, int skip, int pageSize)
        {
            var result = TopSkip.Create(skip, pageSize);
            return result.IsFailure ? Result<OrderByTopSkip, string>.Fail(result.Error) : Result<OrderByTopSkip, string>.Ok(new OrderByTopSkip(sort, result.Value));
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