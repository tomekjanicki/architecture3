﻿namespace Architecture3.Logic.CQ.Product.FilterPaged
{
    using System.Collections.Generic;
    using System.Linq;
    using Architecture3.Common.Database;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types;
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
            var countQuery = string.Format(CountQuery, whereFragment.Where);
            var selectQuery = string.Format(SelectQuery, whereFragment.Where, pagedFragment.Data);
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var count = connection.Query<int>(countQuery, whereFragment.Parameters).Single();
                whereFragment.Parameters.AddDynamicParams(pagedFragment.Parameters);
                var select = connection.Query<Product>(selectQuery, whereFragment.Parameters);
                return Paged<Product>.CreateAndEnsureIsNotFaliure(count, select.ToList());
            }
        }

        private static CommandHelper.WhereResult GetWhereFragment(string code, string name)
        {
            var dp = new DynamicParameters();
            var criteria = new List<NonEmptyString>();
            var codeResult = NonEmptyString.Create(code, (NonEmptyString)"Value");
            var nameResult = NonEmptyString.Create(name, (NonEmptyString)"Value");

            if (codeResult.IsSuccess)
            {
                CommandHelper.SetValues(criteria, dp, CommandHelper.GetLikeCaluse((NonEmptyString)nameof(Product.Code), (NonEmptyString)nameof(Product.Code), codeResult.Value));
            }

            if (nameResult.IsSuccess)
            {
                CommandHelper.SetValues(criteria, dp, CommandHelper.GetLikeCaluse((NonEmptyString)nameof(Product.Name), (NonEmptyString)nameof(Product.Name), nameResult.Value));
            }

            return CommandHelper.GetWhereStringWithParams(criteria, dp);
        }

        private NonEmptyString GetTranslatedSort(string modelColumn)
        {
            var allowedColumns = new[]
            {
                (NonEmptyString)nameof(Product.Id),
                (NonEmptyString)nameof(Product.Code),
                (NonEmptyString)nameof(Product.Name),
                (NonEmptyString)nameof(Product.Price),
                (NonEmptyString)nameof(Product.Date),
                (NonEmptyString)nameof(Product.Version),
                (NonEmptyString)nameof(Product.CanDelete)
            };
            return CommandHelper.GetTranslatedSort(modelColumn, (NonEmptyString)$"{nameof(Product.Code)} ASC", allowedColumns);
        }
    }
}
