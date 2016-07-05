namespace Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public interface IUpdateRepository<in T>
    {
        bool ExistsById(NonNegativeInt id);

        Maybe<NonEmptyString> GetRowVersionById(NonNegativeInt id);

        void Update(T command);
    }
}