namespace Architecture3.Common.Handlers.Interfaces
{
    public interface IHandler<out TResponse>
    {
        TResponse Handle();
    }
}