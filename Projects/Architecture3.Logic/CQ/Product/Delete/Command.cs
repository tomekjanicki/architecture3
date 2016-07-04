namespace Architecture3.Logic.CQ.Product.Delete
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<Result<Error>>, IIdVersion
    {
        private Command(IdVersion idVersion)
        {
            IdVersion = idVersion;
        }

        public IdVersion IdVersion { get; }

        public static Result<Command, string> Create(int id, string version)
        {
            var result = IdVersion.Create(id, version);
            return result.IsFailure ? Result<Command, string>.Fail(result.Error) : Result<Command, string>.Ok(new Command(result.Value));
        }

        protected override bool EqualsCore(Command other)
        {
            return IdVersion == other.IdVersion;
        }

        protected override int GetHashCodeCore()
        {
            return IdVersion.GetHashCode();
        }
    }
}
