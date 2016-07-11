namespace Architecture3.Logic.CQ.Apis.Product.Put
{
    using Architecture3.Logic.CQ.TemplateMethods.Commands;
    using Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces;

    public sealed class CommandHandler : UpdateCommandHandlerTemplate<Command, IUpdateRepository<Command>>
    {
        public CommandHandler(IUpdateRepository<Command> repository)
            : base(repository)
        {
        }
    }
}
