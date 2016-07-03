namespace Architecture3.Logic.CQ.Product.Delete.Interfaces
{
    using Architecture3.Types;

    public interface IRepository
    {
        bool ExistsById(NonNegativeInt id);

        string GetRowVersionById(NonNegativeInt id);

        void Delete(NonNegativeInt id);
    }
}