namespace Architecture3.Logic.CQ.Apis.Product.Post
{
    using System.Linq;
    using Architecture3.Logic.CQ.Apis.Product.Post.Interfaces;
    using Architecture3.Logic.CQ.Apis.Product.ValueObjects;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types;
    using Dapper;

    public class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool CodeExists(Code code)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.Query<bool>("X", new { code = code.Value }).Single();
            }
        }

        public NonNegativeInt Insert(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}
