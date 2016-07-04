namespace Architecture3.Logic.Database
{
    using System.Data;
    using Architecture3.Common.Database;
    using Architecture3.Logic.Database.Interfaces;

    public sealed class DbConnectionProvider : IDbConnectionProvider
    {
        public IDbConnection GetOpenDbConnection()
        {
            return DatabaseExtension.GetOpenConnection("Main");
        }
    }
}