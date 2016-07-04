namespace Architecture3.Logic.CQ.Product.Post.Interfaces
{
    using Architecture3.Logic.CQ.Product.ValueObjects;

    public interface IRepository
    {
        bool CodeExists(Code code);

        int Insert(Command command);
    }
}