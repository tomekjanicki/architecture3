namespace Architecture3.Common.Handlers.Internal
{
    internal abstract class AbstractHandlerWrapper<TResult>
    {
        public abstract TResult Handle();
    }
}
