namespace Architecture3.Common.CQ
{
    using Architecture3.Types.FunctionalExtensions;

    public abstract class EmptyQueryCommand : ValueObject<EmptyQueryCommand>
    {
        protected override bool EqualsCore(EmptyQueryCommand other)
        {
            return this == other;
        }

        protected override int GetHashCodeCore()
        {
            return GetHashCode();
        }
    }
}
