namespace Architecture3.Logic.Database.Interfaces
{
    using System.Data;

    public interface IDbConnectionProvider
    {
        IDbConnection GetOpenDbConnection();
    }
}