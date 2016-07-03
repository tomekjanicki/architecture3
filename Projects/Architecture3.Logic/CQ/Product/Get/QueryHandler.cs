namespace Architecture3.Logic.CQ.Product.Get
{
    using System.Linq;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types.FunctionalExtensions;
    using Dapper;

    public class QueryHandler : IRequestHandler<Query, Maybe<Product>>
    {
        private const string SelectQuery = @"SELECT ID, CODE, NAME, PRICE, VERSION, CASE WHEN ID < 20 THEN GETDATE() ELSE NULL END DATE, CASE WHEN O.PRODUCTID IS NULL THEN 1 ELSE 0 END CANDELETE FROM DBO.PRODUCTS P LEFT JOIN (SELECT DISTINCT PRODUCTID FROM DBO.ORDERSDETAILS) O ON P.ID = O.PRODUCTID WHERE P.ID = @ID";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public QueryHandler(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Maybe<Product> Handle(Query message)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                // todo sql VERSION should be returend as string probably base64
                var select = connection.Query<Product>(SelectQuery, new { ID = message.Id });
                return select.SingleOrDefault();
            }
        }
    }
}
