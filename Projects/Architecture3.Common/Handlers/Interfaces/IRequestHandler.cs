namespace Architecture3.Common.Handlers.Interfaces
{
    public interface IRequestHandler<out TResponse>
    {
        TResponse Handle();
    }

    public interface IRequestHandler<in TRequest, out TResponse>
        where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest message);
    }
}