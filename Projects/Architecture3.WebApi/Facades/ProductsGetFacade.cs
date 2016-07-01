namespace Architecture3.WebApi.Facades
{
    using System;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.Product.Get;
    using Architecture3.Logic.Product.Get.Interfaces;
    using Product = Architecture3.WebApi.Dtos.Product.Get.Product;

    public class ProductsGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IResultMapper _resultMapper;

        public ProductsGetFacade(IMediator mediator, IResultMapper resultMapper)
        {
            _mediator = mediator;
            _resultMapper = resultMapper;
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

            var data = _resultMapper.Map(result.Value);

            return new Tuple<ResponseType, string, Product>(ResponseType.Ok, null, data);
        }
    }
}