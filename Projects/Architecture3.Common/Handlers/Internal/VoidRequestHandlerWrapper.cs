namespace Architecture3.Common.Handlers.Internal
{
    using Architecture3.Common.Handlers.Interfaces;

    internal abstract class VoidRequestHandlerWrapper
    {
        public abstract void Handle(IRequest message);
    }
}