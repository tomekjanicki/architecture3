namespace Architecture3.Logic.CQ.Version.Get
{
    using System.Reflection;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Query : ValueObject<Query>, IRequest<NonEmptyString>
    {
        private Query(Assembly assembly)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; }

        public static Result<Query, NonEmptyString> Create(Assembly assembly)
        {
            return GetOkResult(new Query(assembly));
        }

        protected override bool EqualsCore(Query other)
        {
            return Assembly == other.Assembly;
        }

        protected override int GetHashCodeCore()
        {
            return Assembly.GetHashCode();
        }
    }
}
