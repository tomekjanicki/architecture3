namespace Architecture3.Logic.CQ.Product.Delete
{
    using Architecture3.Logic.CQ.Product.Delete.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Commands;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class CommandHandler : DeleteCommandHandlerTemplate<Command, IRepository>
    {
        public CommandHandler(IRepository repository)
            : base(repository)
        {
        }

        protected override Result<string> BeforeDelete(Command message)
        {
            var id = message.IdVersion.Id;

            var canBeDeleted = DeleteRepository.CanBeDeleted(id);

            return !canBeDeleted ? Result<string>.Fail("Can't delete because there are rows dependent on that item") : base.BeforeDelete(message);
        }
    }
}
