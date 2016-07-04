namespace Architecture3.Logic.CQ.Product.ValueObjects
{
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Name : ValueObject<Name>
    {
        private Name(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static explicit operator Name(string value)
        {
            return Create(value).Value;
        }

        public static implicit operator string(Name name)
        {
            return name.Value;
        }

        public static Result<Name, string> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result<Name, string>.Fail("Name can't be empty");
            }

            const int max = 100;

            return name.Length > max ? Result<Name, string>.Fail($"Name can't be longer than {max} chars.") : Result<Name, string>.Ok(new Name(name));
        }

        protected override bool EqualsCore(Name other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
