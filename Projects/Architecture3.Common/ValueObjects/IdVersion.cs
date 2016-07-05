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

        public static Result<IdVersion, string> Create(int id, string version, string idField, string versionField)
        {
            var idResult = NonNegativeInt.Create(id, idField);

            var result = ResultExtensions.CombineFailures(new[]
            {
                idResult,
                version == string.Empty ? GetFailResult("{0} can't be null or empty", versionField) : (IResult<string>)null
            });

            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new IdVersion(idResult.Value, version));
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
