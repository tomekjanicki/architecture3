namespace Architecture3.Types.FunctionalExtensions
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected static Result<T, string> GetFailResult(NonEmptyString message, NonEmptyString field)
        {
            return Result<T, string>.Fail(string.Format(message, field));
        }

        protected static Result<T, string> GetFailResult(NonEmptyString message)
        {
            return Result<T, string>.Fail(message);
        }

        protected static Result<T, string> GetOkResult(T value)
        {
            return Result<T, string>.Ok(value);
        }

        protected abstract bool EqualsCore(T other);

        protected abstract int GetHashCodeCore();

        protected int GetCalculatedHashCode(IEnumerable<object> list)
        {
            return list.Aggregate(13, (current, i) => current * 7 + i.GetHashCode());
        }
    }
}
