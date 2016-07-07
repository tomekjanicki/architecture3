namespace Architecture3.Logic.CQ.Product.Delete
{
    using System.Linq;
    using Architecture3.Logic.CQ.Product.Delete.Interfaces;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Dapper;

    public sealed class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;
        private readonly SharedQueries _sharedQueries;

        public Repository(IDbConnectionProvider dbConnectionProvider, SharedQueries sharedQueries)
        {
            _dbConnectionProvider = dbConnectionProvider;
            _sharedQueries = sharedQueries;
        }

        public bool ExistsById(NonNegativeInt id)
        {
            return _sharedQueries.ExistsById(id);
        }

        public Maybe<NonEmptyString> GetRowVersionById(NonNegativeInt id)
        {
            return _sharedQueries.GetRowVersionById(id);
        }

        public bool CanBeDeleted(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.Query<bool>("x", new { id = id.Value }).Single();
            }
        }

        public void Delete(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute("DELETE FROM DBO.PRODUCTS WHERE ID = @ID", new { id = id.Value });
            }
        }
    }
}
