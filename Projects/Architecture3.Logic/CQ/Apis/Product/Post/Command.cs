﻿namespace Architecture3.Logic.CQ.Apis.Product.Post
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Apis.Product.ValueObjects;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<IResult<NonNegativeInt, Error>>
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

        public static IResult<Command, NonEmptyString> Create(string name, string code, decimal? price)
        {
            var nameResult = Name.Create(name, (NonEmptyString)nameof(Name));
            var codeResult = Code.Create(code, (NonEmptyString)nameof(Code));
            var priceResult = NonNegativeDecimal.Create(price, (NonEmptyString)nameof(Price));

            var result = new IResult<NonEmptyString>[]
            {
                codeResult,
                priceResult,
                nameResult
            }.IfAtLeastOneFailCombineElseReturnOk();

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
