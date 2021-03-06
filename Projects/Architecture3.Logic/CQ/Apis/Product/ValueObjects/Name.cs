﻿namespace Architecture3.Logic.CQ.Apis.Product.ValueObjects
{
    using Architecture3.Types;
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
            return Create(value, (NonEmptyString)"Value").Value;
        }

        public static implicit operator string(Name name)
        {
            return name.Value;
        }

        public static IResult<Name, NonEmptyString> Create(string name, NonEmptyString field)
        {
            if (name == string.Empty)
            {
                return GetFailResult((NonEmptyString)"{0} can't be empty", field);
            }

            const int max = 100;

            return name.Length > max ? GetFailResult((NonEmptyString)$"{{0}} can't be longer than {max} chars.", field) : GetOkResult(new Name(name));
        }

        public override string ToString()
        {
            return Value;
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
