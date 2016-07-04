namespace Architecture3.Logic.CQ.Product.Post
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.ValueObjects;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<Result<int, Error>>
    {
        private Command(Name name, Code code, NonNegativeDecimal price)
        {
            Name = name;
            Code = code;
            Price = price;
        }

        public Name Name { get; }

        public Code Code { get; }

        public NonNegativeDecimal Price { get; }

        public static Result<Command, string> Create(string name, string code, decimal? price)
        {
            var nameResult = Name.Create(name);
            if (nameResult.IsFailure)
            {
                return Result<Command, string>.Fail(nameResult.Error);
            }

            var codeResult = Code.Create(name);
            if (codeResult.IsFailure)
            {
                return Result<Command, string>.Fail(codeResult.Error);
            }

            if (price == null)
            {
                return Result<Command, string>.Fail("price can't be null");
            }

            var priceResult = NonNegativeDecimal.Create(price.Value);
            if (priceResult.IsFailure)
            {
                return Result<Command, string>.Fail(priceResult.Error);
            }

            return Result<Command, string>.Ok(new Command(nameResult.Value, codeResult.Value, priceResult.Value));
        }

        protected override bool EqualsCore(Command other)
        {
            return Name == other.Name && Code == other.Code && Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Name, Code, Price });
        }
    }
}
