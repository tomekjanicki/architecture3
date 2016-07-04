namespace Architecture3.Logic.CQ.Product.Delete
{
    using System;
    using System.Linq;
    using Architecture3.Logic.CQ.Product.Delete.Interfaces;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types;
    using Dapper;

    public sealed class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool ExistsById(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.Query<bool>("x", new { id }).Single();
            }
        }

        public string GetRowVersionById(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var result = connection.Query<byte[]>("SELECT VERSION FROM DBO.PRODUCTS WHERE ID = @ID", new { id }).SingleOrDefault();
                return Convert.ToBase64String(result);
            }
        }

        public bool CanBeDeleted(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.Query<bool>("x", new { id }).Single();
            }
        }

        public void Delete(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute("DELETE FROM DBO.PRODUCTS WHERE ID = @ID", new { id });
            }
        }
    }
}
