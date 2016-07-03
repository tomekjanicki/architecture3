namespace Architecture3.Logic.Facades
{
    using System.Reflection;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Version;
    using Architecture3.Types.FunctionalExtensions;

    public class VersionGetFacade
    {
        private readonly IMediator _mediator;

        public VersionGetFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Result<string, Error> Get(Assembly assembly)
        {
            var queryResult = Query.Create(assembly);

            if (queryResult.IsFailure)
            {
                return Result<string, Error>.Fail(Error.CreateBadRequest(queryResult.Error));
            }

            var result = _mediator.Send(queryResult.Value);

            return Result<string, Error>.Ok(result);
        }
    }
}
