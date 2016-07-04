namespace Architecture3.Logic.CQ.Product.Delete
{
    using Architecture3.Logic.CQ.Product.Delete.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Commands;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class CommandHandler : DeleteCommandHandler<Command, IRepository>
    {
        public CommandHandler(IRepository repository)
            : base(repository)
        {
        }

        protected override Result<Error> BeforeDelete(Command message)
        {
            var id = message.IdVersion.Id;

            var canBeDeleted = DeleteRepository.CanBeDeleted(id);

            return !canBeDeleted ? Result<Error>.Fail(Error.CreateBadRequest("Can't delete because there are rows dependent on that item")) : base.BeforeDelete(message);
        }
    }
}
