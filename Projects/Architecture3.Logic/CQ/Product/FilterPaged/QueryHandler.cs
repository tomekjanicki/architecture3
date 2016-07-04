namespace Architecture3.Logic.CQ.Product.FilterPaged
{
    using System.Collections.Generic;
    using System.Linq;
    using Architecture3.Common.Database;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types.FunctionalExtensions;
    using Dapper;

    public sealed class QueryHandler : IRequestHandler<Query, Paged<Product>>
    {
        private const string SelectQuery = @"SELECT ID, CODE, NAME, PRICE, VERSION VERSIONPRIVATE, CASE WHEN ID < 20 THEN GETDATE() ELSE NULL END DATE, CASE WHEN O.PRODUCTID IS NULL THEN 1 ELSE 0 END CANDELETE FROM DBO.PRODUCTS P LEFT JOIN (SELECT DISTINCT PRODUCTID FROM DBO.ORDERSDETAILS) O ON P.ID = O.PRODUCTID {0} {1}";
        private const string CountQuery = @"SELECT COUNT(*) FROM DBO.PRODUCTS {0}";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public QueryHandler(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Paged<Product> Handle(Query query)
        {
            var whereFragment = GetWhereFragment(query.Code, query.Name);
            var pagedFragment = CommandHelper.GetPagedFragment(query.OrderByTopSkip.TopSkip, GetTranslatedSort(query.OrderByTopSkip.OrderBy));
            var countQuery = string.Format(CountQuery, whereFragment.Query);
            var selectQuery = string.Format(SelectQuery, whereFragment.Query, pagedFragment.Query);
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var count = connection.Query<int>(countQuery, whereFragment.Parameters).Single();
                whereFragment.Parameters.AddDynamicParams(pagedFragment.Parameters);
                var select = connection.Query<Product>(selectQuery, whereFragment.Parameters);
                var result = Paged<Product>.Create(count, select.ToList());
                result.EnsureIsNotFaliure();
                return result.Value;
            }
        }

        private static CommandHelper.Result GetWhereFragment(string code, string name)
        {
            var dp = new DynamicParameters();
            var criteria = new List<string>();
            if (!string.IsNullOrEmpty(code))
            {
                CommandHelper.SetValues(criteria, dp, CommandHelper.GetLikeCaluse(nameof(Product.Code), nameof(Product.Code), code));
            }

            if (!string.IsNullOrEmpty(name))
            {
                CommandHelper.SetValues(criteria, dp, CommandHelper.GetLikeCaluse(nameof(Product.Name), nameof(Product.Name), name));
            }

            return CommandHelper.GetWhereStringWithParams(criteria, dp);
        }

        private string GetTranslatedSort(string modelColumn)
        {
            return CommandHelper.GetTranslatedSort(modelColumn, $"{nameof(Product.Code)} ASC", new[] { nameof(Product.Id), nameof(Product.Code), nameof(Product.Name), nameof(Product.Price), nameof(Product.Date), nameof(Product.Version), nameof(Product.CanDelete) });
        }
    }
}
