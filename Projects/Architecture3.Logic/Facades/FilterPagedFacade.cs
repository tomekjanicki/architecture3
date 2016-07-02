﻿namespace Architecture3.Logic.Facades
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Product.FilterPaged;
    using Architecture3.Types.FunctionalExtensions;
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

        public Result<Paged<Product>, Error> FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var queryResult = Query.Create(orderBy, skip, top, filter);

            if (queryResult.IsFailure)
            {
                return Result<Paged<Product>, Error>.Fail(Error.CreateBadRequest(queryResult.Error));
            }

            var result = _mediator.Send(queryResult.Value);

            var data = _mapper.Map<Paged<Product>>(result);

            return Result<Paged<Product>, Error>.Ok(data);
        }
    }
}