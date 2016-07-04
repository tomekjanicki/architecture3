namespace Architecture3.Logic.CQ.Product.Delete.Interfaces
{
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Types;

    public interface IRepository : IDeleteRepository
    {
        bool CanBeDeleted(NonNegativeInt id);
    }
}