namespace Architecture3.Logic.Product.Get
{
    using System.Linq;
    using Architecture3.Logic.Interfaces;
    using Architecture3.Logic.Product.Get.Interfaces;
    using Architecture3.Types.FunctionalExtensions;
    using Dapper;

    public class Repository : IRepository
    {
        private const string SelectQuery = @"SELECT ID, CODE, NAME, PRICE, VERSION, CASE WHEN ID < 20 THEN GETDATE() ELSE NULL END DATE, CASE WHEN O.PRODUCTID IS NULL THEN 1 ELSE 0 END CANDELETE FROM DBO.PRODUCTS P LEFT JOIN (SELECT DISTINCT PRODUCTID FROM DBO.ORDERSDETAILS) O ON P.ID = O.PRODUCTID WHERE P.ID = @ID";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Maybe<Product> Fetch(Query query)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var select = connection.Query<Product>(SelectQuery, new { ID = query.Id });
                return select.SingleOrDefault();
            }
        }
    }
}