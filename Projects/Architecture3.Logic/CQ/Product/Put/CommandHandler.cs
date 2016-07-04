namespace Architecture3.Logic.CQ.Product.Put
{
    using Architecture3.Logic.CQ.Product.Put.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Commands;

    public sealed class CommandHandler : UpdateCommandHandlerTemplate<Command, IRepository>
    {
        public CommandHandler(IRepository repository)
            : base(repository)
        {
        }
    }
}
