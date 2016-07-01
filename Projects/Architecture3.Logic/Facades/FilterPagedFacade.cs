namespace Architecture3.Logic.Facades
{
    using System;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Product.FilterPaged;
    using Architecture3.WebApi.Dtos;
    using AutoMapper;

    public class FilterPagedFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FilterPagedFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public Tuple<ResponseType, string, Paged<WebApi.Dtos.Product.FilterPaged.Product>> FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var queryResult = Query.Create(orderBy, skip, top, filter);

            if (queryResult.IsFailure)
            {
                return new Tuple<ResponseType, string, Paged<WebApi.Dtos.Product.FilterPaged.Product>>(ResponseType.BadRequest, queryResult.Error, null);
            }

            var result = _mediator.Send(queryResult.Value);

            var data = _mapper.Map<Paged<WebApi.Dtos.Product.FilterPaged.Product>>(result);

            return new Tuple<ResponseType, string, Paged<WebApi.Dtos.Product.FilterPaged.Product>>(ResponseType.Ok, null, data);
        }
    }
}