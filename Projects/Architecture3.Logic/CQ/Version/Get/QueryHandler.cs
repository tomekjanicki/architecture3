namespace Architecture3.Logic.CQ.Version.Get
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.Tools.Interfaces;
    using Architecture3.Types;

    public sealed class QueryHandler : IRequestHandler<Query, NonEmptyString>
    {
        private readonly IAssemblyVersionProvider _assemblyVersionProvider;

        public QueryHandler(IAssemblyVersionProvider assemblyVersionProvider)
        {
            _assemblyVersionProvider = assemblyVersionProvider;
        }

        public NonEmptyString Handle(Query message)
        {
            return (NonEmptyString)_assemblyVersionProvider.Get(message.Assembly).ToString();
        }
    }
}
