namespace Architecture3.Common.Handlers.Internal
{
    using Architecture3.Common.Handlers.Interfaces;

    internal sealed class HandlerWrapper<TResult> : AbstractHandlerWrapper<TResult>
    {
        private readonly IRequestHandler<TResult> _inner;

        public HandlerWrapper(IRequestHandler<TResult> inner)
        {
            _inner = inner;
        }

        public override TResult Handle()
        {
            return _inner.Handle();
        }
    }
}