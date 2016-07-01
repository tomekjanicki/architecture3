namespace Architecture3.Logic
{
    using System.Data;
    using Architecture3.Common.Database;
    using Architecture3.Logic.Interfaces;

    public class DbConnectionProvider : IDbConnectionProvider
    {
        public IDbConnection GetOpenDbConnection()
        {
            return DatabaseExtension.GetConnection("Main");
        }
    }
}