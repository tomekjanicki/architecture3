namespace Architecture3.Logic.CQ.Pages.Home.Index
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Types;

    public sealed class QueryHandler : IRequestHandler<Query, NonEmptyString>
    {
        public NonEmptyString Handle(Query message)
        {
            return (NonEmptyString)"Index";
        }
    }
}
