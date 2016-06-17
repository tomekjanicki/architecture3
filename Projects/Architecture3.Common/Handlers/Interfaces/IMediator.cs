namespace Architecture3.Common.Handlers.Interfaces
{
    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);

        void Send(IRequest request);

        TResponse Send<TResponse>();
    }
}