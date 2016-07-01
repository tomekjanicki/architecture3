namespace Architecture3.Logic.Product.FilterPaged
{
    using Architecture3.Logic.Product.FilterPaged.Interfaces;
    using Architecture3.WebApi.Dtos;
    using AutoMapper;

    public class ResultMapper : IResultMapper
    {
        private readonly IMapper _mapper;

        public ResultMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Paged<WebApi.Dtos.Product.FilterPaged.Product> Map(Common.ValueObjects.Paged<Product> input)
        {
            return _mapper.Map<Paged<WebApi.Dtos.Product.FilterPaged.Product>>(input);
        }
    }
}
