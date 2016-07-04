namespace Architecture3.Logic.CQ.Product.Post
{
    using Architecture3.Logic.CQ.Product.ValueObjects;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>
    {
        private Command(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        protected override bool EqualsCore(Command other)
        {
            return Name == other.Name;
        }

        protected override int GetHashCodeCore()
        {
            throw new System.NotImplementedException();
        }
    }
}
