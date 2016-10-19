namespace Architecture3.Logic.CQ.Apis
{
    using System.Collections.Generic;
    using System.Linq;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public class OrderByParser
    {
        public NonEmptyString GetInvalidOrderExpressionMessage(NonEmptyString column, IEnumerable<NonEmptyString> allowedColumns)
        {
            return (NonEmptyString)("Invalid order expression (it has not allowed colum). Column: " + column + ". Allowed columns: " + string.Join(", ", allowedColumns) + ".");
        }

        public NonEmptyString GetInvalidOrderExpressionMessage(string orderBy)
        {
            return (NonEmptyString)("Invalid order expression (it has empty elements). Expression: " + orderBy + ".");
        }

        public IResult<IReadOnlyCollection<OrderBy>, NonEmptyString> Parse(string orderBy, IEnumerable<NonEmptyString> allowedColumns)
        {
            var result = new List<OrderBy>();

            var orderByLowerCaseTrimmed = orderBy.ToLower().Trim();

            if (orderByLowerCaseTrimmed == string.Empty)
            {
                return result.GetOkMessage();
            }

            var allowedColumnsLowerCase = allowedColumns.Select(s => (NonEmptyString)s.Value.ToLower()).ToList();

            var orderByArray = orderByLowerCaseTrimmed.Split(',');

            foreach (var orderByItem in orderByArray)
            {
                var orderByItemLowerCaseTrimmed = orderByItem.ToLower().Trim();

                if (orderByItemLowerCaseTrimmed != string.Empty)
                {
                    var nonEmptyOrderByItemLowerCaseTrimmed = (NonEmptyString)orderByItemLowerCaseTrimmed;

                    var order = GetOrder(nonEmptyOrderByItemLowerCaseTrimmed);

                    if (order != null)
                    {
                        var column = nonEmptyOrderByItemLowerCaseTrimmed.Value.Substring(1).Trim();

                        if (column != string.Empty)
                        {
                            var nonEmptyColumm = (NonEmptyString)column;

                            if (allowedColumnsLowerCase.Contains(nonEmptyColumm))
                            {
                                result.Add(OrderBy.Create(nonEmptyColumm, order.Value));
                            }
                            else
                            {
                                return GetInvalidOrderExpressionMessage(nonEmptyOrderByItemLowerCaseTrimmed, allowedColumnsLowerCase).GetFailResult<IReadOnlyCollection<OrderBy>>();
                            }
                        }
                        else
                        {
                            return GetInvalidOrderExpressionMessage(orderBy).GetFailResult<IReadOnlyCollection<OrderBy>>();
                        }
                    }
                    else
                    {
                        if (allowedColumnsLowerCase.Contains(nonEmptyOrderByItemLowerCaseTrimmed))
                        {
                            result.Add(OrderBy.Create(nonEmptyOrderByItemLowerCaseTrimmed, true));
                        }
                        else
                        {
                            return GetInvalidOrderExpressionMessage(nonEmptyOrderByItemLowerCaseTrimmed, allowedColumnsLowerCase).GetFailResult<IReadOnlyCollection<OrderBy>>();
                        }
                    }
                }
                else
                {
                    return GetInvalidOrderExpressionMessage(orderBy).GetFailResult<IReadOnlyCollection<OrderBy>>();
                }
            }

            return result.GetOkMessage();
        }

        private static bool? GetOrder(NonEmptyString nonEmptyOrderByItemLowerCaseTrimmed)
        {
            if (nonEmptyOrderByItemLowerCaseTrimmed.Value.StartsWith("+"))
            {
                return true;
            }

            if (nonEmptyOrderByItemLowerCaseTrimmed.Value.StartsWith("-"))
            {
                return false;
            }

            return null;
        }
    }
}