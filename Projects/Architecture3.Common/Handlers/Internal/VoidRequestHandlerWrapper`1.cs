namespace Architecture3.Common.Handlers.Internal
{
    using Architecture3.Common.Handlers.Interfaces;

    internal sealed class VoidRequestHandlerWrapper<TCommand> : VoidRequestHandlerWrapper
        where TCommand : IRequest
    {
        private readonly IRequestHandler<TCommand> _inner;

        public VoidRequestHandlerWrapper(IRequestHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public override void Handle(IRequest message)
        {
            _inner.Handle((TCommand)message);
        }
    }
}