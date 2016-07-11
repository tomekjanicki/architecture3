namespace Architecture3.Logic.Facades.Apis
{
    using System.Reflection;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Apis.Version.Get;
    using Architecture3.Logic.Facades.Base;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using AutoMapper;

    public sealed class VersionGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public VersionGetFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<string, Error> Get(Assembly assembly)
        {
            var queryResult = Query.Create(assembly);

            return Helper.GetItemSimple<string, Query, NonEmptyString>(_mediator, _mapper, queryResult);
        }
    }
}
