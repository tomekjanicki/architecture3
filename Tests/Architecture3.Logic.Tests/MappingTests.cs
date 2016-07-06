namespace Architecture3.Logic.Tests
{
    using Architecture3.Logic.CQ.Product.Get;
    using AutoMapper;
    using NUnit.Framework;
    using Shouldly;

    public class MappingTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var configuration = new MapperConfiguration(AutoMapperConfiguration.Configure);
            configuration.AssertConfigurationIsValid();
            _mapper = configuration.CreateMapper();
        }

        [Test]
        public void Product_Should_Map_To_ProductDto()
        {
            const int id = 1;
            const string code = "code";
            const string name = "name";
            var source = new Product
            {
                Id = id,
                Code = code,
                Name = name
            };
            var result = _mapper.Map<WebApi.Dtos.Product.Get.Product>(source);
            result.Id.ShouldBe(id);
            result.Code.ShouldBe(code);
            result.Name.ShouldBe(name);
            result.Version.ShouldBe(string.Empty);
        }
    }
}
