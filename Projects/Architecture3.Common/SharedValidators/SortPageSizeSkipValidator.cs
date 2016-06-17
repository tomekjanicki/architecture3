namespace Architecture3.Common.SharedValidators
{
    using Architecture3.Common.FluentValidation;
    using Architecture3.Common.SharedStructs.RequestParams;
    using global::FluentValidation;

    public class SortPageSizeSkipValidator<TItem> : AbstractClassValidator<SortPageSizeSkip<TItem>>
    {
        public SortPageSizeSkipValidator()
        {
            RuleFor(query => query.PageSize).InclusiveBetween(Const.MinPageSize, Const.MaxPageSize);
            RuleFor(query => query.Skip).InclusiveBetween(0, int.MaxValue);
        }
    }
}