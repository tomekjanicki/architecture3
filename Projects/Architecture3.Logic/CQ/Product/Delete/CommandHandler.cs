namespace Architecture3.Logic.CQ.Product.Delete
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public class CommandHandler : IRequestHandler<Command, Result<Error>>
    {
        public Result<Error> Handle(Command message)
        {
            return Result<Error>.Ok();
        }
    }
}
