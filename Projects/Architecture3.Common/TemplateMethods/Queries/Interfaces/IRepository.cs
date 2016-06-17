namespace Architecture3.Common.TemplateMethods.Queries.Interfaces
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.SharedStructs.ResponseParams;

    public interface IRepository<TItem, in TParam>
        where TParam : IRequest<Result<TItem>>
    {
        Result<TItem> Fetch(TParam query);
    }
}
