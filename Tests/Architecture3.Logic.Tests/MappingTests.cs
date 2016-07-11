namespace Architecture3.Logic.Tests
{
    using Architecture3.Logic.CQ.Apis.Product.Get;
    using Architecture3.Logic.CQ.Apis.Product.ValueObjects;
    using Architecture3.Types;
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
            const decimal price = 5.22M;
            var source = new Product
            {
                Id = (NonNegativeInt)id,
                Code = (Code)code,
                Name = (Name)name,
                Price = (NonNegativeDecimal)price
            };
            var result = _mapper.Map<Web.Dtos.Apis.Product.Get.Product>(source);
            result.Id.ShouldBe(id);
            result.Code.ShouldBe(code);
            result.Name.ShouldBe(name);
            result.Price.ShouldBe(price);
            result.Version.ShouldNotBeNullOrEmpty();
        }
    }
}
