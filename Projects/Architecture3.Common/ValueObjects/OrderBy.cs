namespace Architecture3.Common.ValueObjects
{
    using System.Collections.Generic;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public class OrderBy : ValueObject<OrderBy>
    {
        private OrderBy(NonEmptyString column, bool order)
        {
            Column = column;
            Order = order;
        }

        public NonEmptyString Column { get; }

        public bool Order { get; }

        public static IResult<OrderBy, NonEmptyString> Create(string column, bool order)
        {
            var columnResult = NonEmptyString.Create(column, (NonEmptyString)nameof(Column));
            return columnResult.IsFailure ? GetFailResult(columnResult.Error) : GetOkResult(new OrderBy(columnResult.Value, order));
        }

        public static OrderBy Create(NonEmptyString column, bool order)
        {
            return new OrderBy(column, order);
        }

        protected override bool EqualsCore(OrderBy other)
        {
            return Column == other.Column && Order == other.Order;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { Column, Order });
        }
    }
}
