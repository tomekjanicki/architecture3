namespace Architecture3.Logic.CQ.TemplateMethods.Queries.Interfaces
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public interface IGetRepository<T>
        where T : class
    {
        Maybe<T> Get(NonNegativeInt id);
    }
}