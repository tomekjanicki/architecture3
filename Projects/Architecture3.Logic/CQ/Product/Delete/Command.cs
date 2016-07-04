namespace Architecture3.Logic.CQ.Product.Delete
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<Result<Error>>
    {
        private Command(NonNegativeInt id, string version)
        {
            Id = id;
            Version = version;
        }

        public NonNegativeInt Id { get; }

        public string Version { get; }

        public static Result<Command, string> Create(int id, string version)
        {
            var idResult = NonNegativeInt.Create(id);
            if (idResult.IsFailure)
            {
                return Result<Command, string>.Fail(idResult.Error);
            }

            return string.IsNullOrEmpty(version) ? Result<Command, string>.Fail("Version can't be empty string") : Result<Command, string>.Ok(new Command(idResult.Value, version));
        }

        protected override bool EqualsCore(Command other)
        {
            return Id == other.Id && Version == other.Version;
        }

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = hash * 7 + Id.GetHashCode();
            hash = hash * 7 + Version.GetHashCode();
            return hash;
        }
    }
}
