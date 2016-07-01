namespace Architecture3.Logic.Interfaces
{
    using System.Data;

    public interface IDbConnectionProvider
    {
        IDbConnection GetOpenDbConnection();
    }
}