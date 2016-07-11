namespace Architecture3.Logic.CQ.Home.Index
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Types;

    public sealed class QueryHandler : IRequestHandler<NonEmptyString>
    {
        public NonEmptyString Handle()
        {
            return (NonEmptyString)"Index";
        }
    }
}
