namespace Architecture3.Logic.CQ.Product.Post
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.ValueObjects;
    using Architecture3.Logic.Shared;
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
            var nameResult = Name.Create(name, nameof(Name));
            var codeResult = Code.Create(name, nameof(Code));
            var priceResult = NonNegativeDecimal.Create(price, nameof(Price));

            var result = ResultExtensions.CombineFailures(new IResult<string>[]
            {
                codeResult,
                priceResult,
                nameResult
            });

            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new Command(nameResult.Value, codeResult.Value, priceResult.Value));
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
