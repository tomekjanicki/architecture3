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
            return Create(value, "Value").Value;
        }

        public static implicit operator string(Name name)
        {
            return name.Value;
        }

        public static Result<Name, string> Create(string name, string field)
        {
            if (string.IsNullOrEmpty(name))
            {
                return GetFailResult("{0} can't be null or empty", field);
            }

            const int max = 100;

            return name.Length > max ? GetFailResult($"{0} can't be longer than {max} chars.", field) : GetOkResult(new Name(name));
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
