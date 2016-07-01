namespace Architecture3.Logic.Product.FilterPaged.Interfaces
{
    using Architecture3.WebApi.Dtos;

    public interface IResultMapper
    {
        Paged<WebApi.Dtos.Product.FilterPaged.Product> Map(Common.ValueObjects.Paged<FilterPaged.Product> input);
    }
}