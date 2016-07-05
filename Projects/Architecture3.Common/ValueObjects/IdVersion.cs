namespace Architecture3.Common.ValueObjects
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class IdVersion : ValueObject<IdVersion>
    {
        private IdVersion(NonNegativeInt id, NonEmptyString version)
        {
            Id = id;
            Version = version;
        }

        public NonNegativeInt Id { get; }

        public NonEmptyString Version { get; }

        public static Result<IdVersion, NonEmptyString> Create(int id, string version, NonEmptyString idField, NonEmptyString versionField)
        {
            var idResult = NonNegativeInt.Create(id, idField);
            var versionResult = NonEmptyString.Create(version, versionField);

            var result = ResultExtensions.CombineFailures(new IResult<NonEmptyString>[]
            {
                idResult,
                versionResult
            });

            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new IdVersion(idResult.Value, versionResult.Value));
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
