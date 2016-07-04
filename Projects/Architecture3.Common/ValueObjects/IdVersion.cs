namespace Architecture3.Common.ValueObjects
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class IdVersion : ValueObject<IdVersion>
    {
        private IdVersion(NonNegativeInt id, string version)
        {
            Id = id;
            Version = version;
        }

        public NonNegativeInt Id { get; }

        public string Version { get; }

        public static Result<IdVersion, string> Create(int id, string version)
        {
            var idResult = NonNegativeInt.Create(id);
            if (idResult.IsFailure)
            {
                return Result<IdVersion, string>.Fail(idResult.Error);
            }

            return string.IsNullOrEmpty(version) ? Result<IdVersion, string>.Fail("Version can't be empty string") : Result<IdVersion, string>.Ok(new IdVersion(idResult.Value, version));
        }

        protected override bool EqualsCore(IdVersion other)
        {
            return Id == other.Id && Version == other.Version;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Id, Version });
        }
    }
}
