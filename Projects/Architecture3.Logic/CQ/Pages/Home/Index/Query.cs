namespace Architecture3.Logic.CQ.Pages.Home.Index
{
    using Architecture3.Common.CQ;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Types;

    public sealed class Query : EmptyQueryCommand, IRequest<NonEmptyString>
    {
        private Query()
        {
        }

        public static Query Create()
        {
            return new Query();
        }
    }
}
