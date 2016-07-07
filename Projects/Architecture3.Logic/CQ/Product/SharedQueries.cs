namespace Architecture3.Logic.CQ.Product
{
    using System;
    using System.Linq;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Dapper;

    public class SharedQueries
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public SharedQueries(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool ExistsById(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.Query<bool>("x", new { id.Value }).Single();
            }
        }

        public Maybe<NonEmptyString> GetRowVersionById(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var result = connection.Query<byte[]>("SELECT VERSION FROM DBO.PRODUCTS WHERE ID = @ID", new { id = id.Value }).SingleOrDefault();
                return result != null ? (NonEmptyString)Convert.ToBase64String(result) : null;
            }
        }
    }
}
