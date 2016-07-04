namespace Architecture3.Logic.CQ.Product.Put
{
    using Architecture3.Logic.CQ.Product.Put.Interfaces;
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

        public Maybe<string> GetRowVersionById(NonNegativeInt id)
        {
            return _sharedQueries.GetRowVersionById(id);
        }

        public void Update(Command command)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute("UPDATE DBO.PRODUCTS WHERE ID = @ID", new { command.IdVersion.Id });
            }
        }
    }
}
