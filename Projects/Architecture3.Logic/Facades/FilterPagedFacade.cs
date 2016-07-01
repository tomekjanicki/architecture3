namespace Architecture3.Logic.Facades
{
    using System;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Product.FilterPaged;
    using Architecture3.Logic.Product.FilterPaged.Interfaces;
    using Architecture3.WebApi.Dtos;

    public class FilterPagedFacade
    {
        private readonly IMediator _mediator;
        private readonly IResultMapper _resultMapper;

        public FilterPagedFacade(IMediator mediator, IResultMapper resultMapper)
        {
            _mediator = mediator;
            _resultMapper = resultMapper;
        }

        public Tuple<ResponseType, string, Paged<WebApi.Dtos.Product.FilterPaged.Product>> FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var queryResult = Query.Create(orderBy, skip, top, filter);

            if (queryResult.IsFailure)
            {
                return new Tuple<ResponseType, string, Paged<WebApi.Dtos.Product.FilterPaged.Product>>(ResponseType.BadRequest, queryResult.Error, null);
            }

            var result = _mediator.Send(queryResult.Value);

            var data = _resultMapper.Map(result);

            return new Tuple<ResponseType, string, Paged<WebApi.Dtos.Product.FilterPaged.Product>>(ResponseType.Ok, null, data);
        }
    }
}