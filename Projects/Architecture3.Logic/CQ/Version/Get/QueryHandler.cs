namespace Architecture3.Logic.CQ.Version.Get
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.Tools.Interfaces;

    public sealed class QueryHandler : IRequestHandler<Query, string>
    {
        private readonly IAssemblyVersionProvider _assemblyVersionProvider;

        public QueryHandler(IAssemblyVersionProvider assemblyVersionProvider)
        {
            _assemblyVersionProvider = assemblyVersionProvider;
        }

        public string Handle(Query message)
        {
            return _assemblyVersionProvider.Get(message.Assembly).ToString();
        }
    }
}
