﻿namespace Architecture3.Logic.CQ.Product.Put
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Logic.CQ.Product.ValueObjects;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<Result<Error>>, IIdVersion
    {
        private Command(IdVersion idVersion, NonNegativeDecimal price, Name name)
        {
            IdVersion = idVersion;
            Price = price;
            Name = name;
        }

        public IdVersion IdVersion { get; }

        public NonNegativeDecimal Price { get; }

        public Name Name { get; }

        public static Result<Command, string> Create(int id, string version, decimal price, string name)
        {
            var idVersionResult = IdVersion.Create(id, version);
            if (idVersionResult.IsFailure)
            {
                return Result<Command, string>.Fail(idVersionResult.Error);
            }

            var priceResult = NonNegativeDecimal.Create(price);
            if (priceResult.IsFailure)
            {
                return Result<Command, string>.Fail(priceResult.Error);
            }

            var nameResult = Name.Create(name);
            if (nameResult.IsFailure)
            {
                return Result<Command, string>.Fail(priceResult.Error);
            }

            return Result<Command, string>.Ok(new Command(idVersionResult.Value, priceResult.Value, nameResult.Value));
        }

        protected override bool EqualsCore(Command other)
        {
            return IdVersion == other.IdVersion && Price == other.Price && Name == other.Name;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { IdVersion, Price, Name });
        }
    }
}