namespace Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public interface IDeleteRepository
    {
        bool ExistsById(NonNegativeInt id);

        Maybe<string> GetRowVersionById(NonNegativeInt id);

        void Delete(NonNegativeInt id);
    }
}