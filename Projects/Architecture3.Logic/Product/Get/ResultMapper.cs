namespace Architecture3.Logic.Product.Get
{
    using Architecture3.Logic.Product.Get.Interfaces;
    using AutoMapper;

    public class ResultMapper : IResultMapper
    {
        private readonly IMapper _mapper;

        public ResultMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public WebApi.Dtos.Product.Get.Product Map(Get.Product input)
        {
            return _mapper.Map<WebApi.Dtos.Product.Get.Product>(input);
        }
    }
}
