namespace Architecture3.Logic.Product.FindPagedCollection
{
    using Architecture3.Common.SharedValidators;
    using Architecture3.WebApi.Dtos.Product.FindPagedCollection;

    public class QueryValidator : SortPageSizeSkipValidator<ProductItem>
    {
    }
}