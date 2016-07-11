namespace Architecture3.Logic.CQ.Apis.Product.Delete
{
    using Architecture3.Logic.CQ.Apis.Product.Delete.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Commands;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class CommandHandler : DeleteCommandHandlerTemplate<Command, IRepository>
    {
        public CommandHandler(IRepository repository)
            : base(repository)
        {
        }

        protected override IResult<NonEmptyString> BeforeDelete(Command message)
        {
            var id = message.IdVersion.Id;

            var canBeDeleted = DeleteRepository.CanBeDeleted(id);

            return !canBeDeleted ? ((NonEmptyString)"Can't delete because there are rows dependent on that item").GetFailResult() : base.BeforeDelete(message);
        }
    }
}
