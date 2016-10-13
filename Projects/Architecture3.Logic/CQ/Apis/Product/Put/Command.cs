namespace Architecture3.Logic.CQ.Apis.Product.Put
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Logic.CQ.Apis.Product.ValueObjects;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<IResult<Error>>, IIdVersion
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

        public static IResult<Command, NonEmptyString> Create(int id, string version, decimal? price, string name)
        {
            var idVersionResult = IdVersion.Create(id, version, (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Id), (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Version));
            var priceResult = NonNegativeDecimal.Create(price, (NonEmptyString)nameof(Price));
            var nameResult = Name.Create(name, (NonEmptyString)nameof(Name));

            var result = ResultExtensions.IfAtLeastOneFailCombineElseReturnOk(new IResult<NonEmptyString>[]
            {
                idVersionResult,
                priceResult,
                nameResult
            });

            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new Command(idVersionResult.Value, priceResult.Value, nameResult.Value));
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
