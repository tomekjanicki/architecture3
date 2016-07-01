namespace Architecture3.Logic.Facades
{
    using System;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Product.Get;
    using AutoMapper;
    using Product = WebApi.Dtos.Product.Get.Product;

    public class ProductsGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsGetFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public Tuple<ResponseType, string, Product> Get(int id)
        {
            var queryResult = Query.Create(id);

            if (queryResult.IsFailure)
            {
                return new Tuple<ResponseType, string, Product>(ResponseType.BadRequest, queryResult.Error, null);
            }

            var result = _mediator.Send(queryResult.Value);

            if (result.HasNoValue)
            {
                return new Tuple<ResponseType, string, Product>(ResponseType.NotFound, null, null);
            }

            var data = _mapper.Map<WebApi.Dtos.Product.Get.Product>(result.Value);

            return new Tuple<ResponseType, string, Product>(ResponseType.Ok, null, data);
        }
    }
}