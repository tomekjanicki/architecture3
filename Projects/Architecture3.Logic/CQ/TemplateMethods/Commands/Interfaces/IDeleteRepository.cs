namespace Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces
{
    using Architecture3.Types;

    public interface IDeleteRepository
    {
        bool ExistsById(NonNegativeInt id);

        string GetRowVersionById(NonNegativeInt id);

        void Delete(NonNegativeInt id);
    }
}