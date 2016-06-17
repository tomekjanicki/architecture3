namespace Architecture3.Logic.Product.FindPagedCollection
{
    using Architecture3.Common.SharedStructs.RequestParams;
    using Architecture3.WebApi.Dtos.Product.FindPagedCollection;

    public class Query : SortPageSizeSkip<ProductItem>
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
