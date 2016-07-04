namespace Architecture3.Logic.CQ.Product.Put
{
    using Architecture3.Logic.CQ.Product.Put.Interfaces;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool ExistsById(NonNegativeInt id)
        {
            throw new System.NotImplementedException();
        }

        public Maybe<string> GetRowVersionById(NonNegativeInt id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}
