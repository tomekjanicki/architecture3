namespace Architecture3.Logic.CQ.Product.Put.Interfaces
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public interface IRepository
    {
        bool ExistsById(NonNegativeInt id);

        Maybe<string> GetRowVersionById(NonNegativeInt id);

        void Update(Command command);
    }
}