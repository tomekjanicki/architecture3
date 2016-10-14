namespace Architecture3.Common.CQ
{
    using Architecture3.Types.FunctionalExtensions;

    public abstract class EmptyCommandQuery : ValueObject<EmptyCommandQuery>
    {
        protected override bool EqualsCore(EmptyCommandQuery other)
        {
            return this == other;
        }

        protected override int GetHashCodeCore()
        {
            return GetHashCode();
        }
    }
}
