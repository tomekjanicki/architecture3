namespace Architecture3.Logic.Product.FindPagedCollection
{
    using Architecture3.Common.SharedStructs.RequestParams;

    public class Query : SortPageSizeSkip<WebApi.Dtos.Product.FilterPaged.Product>
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
