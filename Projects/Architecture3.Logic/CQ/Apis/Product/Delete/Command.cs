﻿namespace Architecture3.Logic.CQ.Apis.Product.Delete
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<IResult<Error>>, IIdVersion
    {
        private Command(IdVersion idVersion)
        {
            IdVersion = idVersion;
        }

        public IdVersion IdVersion { get; }

        public static IResult<Command, NonEmptyString> Create(int id, string version)
        {
            var result = IdVersion.Create(id, version, (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Id), (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Version));
            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new Command(result.Value));
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
