namespace Architecture3.Logic.CQ.Apis.Product.Post.Interfaces
{
    using Architecture3.Logic.CQ.Apis.Product.ValueObjects;
    using Architecture3.Types;

    public interface IRepository
    {
        bool CodeExists(Code code);

        NonNegativeInt Insert(Command command);
    }
}