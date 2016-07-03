namespace Architecture3.Logic.Version
{
    using System.Reflection;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Types.FunctionalExtensions;

    public class Query : ValueObject<Query>, IRequest<string>
    {
        private Query(Assembly assembly)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; }

        public static Result<Query, string> Create(Assembly assembly)
        {
            return Result<Query, string>.Ok(new Query(assembly));
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
