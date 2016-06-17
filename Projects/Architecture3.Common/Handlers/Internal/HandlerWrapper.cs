namespace Architecture3.Common.Handlers.Internal
{
    using Architecture3.Common.Handlers.Interfaces;

    internal class HandlerWrapper<TResult> : AbstractHandlerWrapper<TResult>
    {
        private readonly IHandler<TResult> _inner;

        public HandlerWrapper(IHandler<TResult> inner)
        {
            _inner = inner;
        }

        public override TResult Handle()
        {
            return _inner.Handle();
        }
    }
}