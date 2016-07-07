namespace Architecture3.Logic.CQ.Product.Get
{
    using System.Linq;
    using Architecture3.Logic.CQ.TemplateMethods.Queries.Interfaces;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Dapper;

    public class Repository : IGetRepository<Product>
    {
        private const string SelectQuery = @"SELECT ID, CODE, NAME, PRICE, VERSION VERSIONPRIVATE, CASE WHEN ID < 20 THEN GETDATE() ELSE NULL END DATE, CASE WHEN O.PRODUCTID IS NULL THEN 1 ELSE 0 END CANDELETE FROM DBO.PRODUCTS P LEFT JOIN (SELECT DISTINCT PRODUCTID FROM DBO.ORDERSDETAILS) O ON P.ID = O.PRODUCTID WHERE P.ID = @ID";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Maybe<Product> Get(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var select = connection.Query<Product>(SelectQuery, new { id = id.Value });
                return select.SingleOrDefault();
            }
        }
    }
}
