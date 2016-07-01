namespace Architecture3.Logic.Product.FilterPaged
{
    using System.Linq;
    using Architecture3.Logic.Product.FilterPaged.Interfaces;
    using Architecture3.WebApi.Dtos;

    public class ResultMapper : IResultMapper
    {
        public Paged<WebApi.Dtos.Product.FilterPaged.Product> Map(Common.ValueObjects.Paged<Product> input)
        {
            var items = input.Items.Select(product => new WebApi.Dtos.Product.FilterPaged.Product
            {
                Code = product.Code,
                CanDelete = product.CanDelete,
                Date = product.Date,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Version = product.Version
            });
            return new Paged<WebApi.Dtos.Product.FilterPaged.Product>(input.Count, items);
        }
    }
}
