namespace Architecture3.Common.Handlers.Internal
{
    using Architecture3.Common.Handlers.Interfaces;

    internal sealed class RequestHandlerWrapper<TCommand, TResult> : RequestHandlerWrapper<TResult>
        where TCommand : IRequest<TResult>
    {
        private readonly IRequestHandler<TCommand, TResult> _inner;

        public RequestHandlerWrapper(IRequestHandler<TCommand, TResult> inner)
        {
            _inner = inner;
        }

        public override TResult Handle(IRequest<TResult> message)
        {
            return _inner.Handle((TCommand)message);
        }
    }
}