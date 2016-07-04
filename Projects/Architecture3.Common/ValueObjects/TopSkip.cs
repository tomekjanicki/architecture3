namespace Architecture3.Common.ValueObjects
{
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class TopSkip : ValueObject<TopSkip>
    {
        private TopSkip(NonNegativeInt skip, GreaterThanZeroInt top)
        {
            Skip = skip;
            Top = top;
        }

        public NonNegativeInt Skip { get; }

        public GreaterThanZeroInt Top { get; }

        public static Result<TopSkip, string> Create(int skip, int pageSize)
        {
            var skipResult = NonNegativeInt.Create(skip);

            var pageSizeResult = GreaterThanZeroInt.Create(pageSize);

            var result = ResultExtensions.CombineFaliures(new IResult<string>[] { skipResult, pageSizeResult });

            return result.IsFailure ? Result<TopSkip, string>.Fail(result.Error) : Create(skipResult.Value, pageSizeResult.Value);
        }

        public static Result<TopSkip, string> Create(NonNegativeInt skip, GreaterThanZeroInt pageSize)
        {
            return pageSize.Value > Const.MaxTopSize ? Result<TopSkip, string>.Fail($"Top can't be greater than {Const.MaxTopSize}") : Result<TopSkip, string>.Ok(new TopSkip(skip, pageSize));
        }

        protected override bool EqualsCore(TopSkip other)
        {
            return Skip == other.Skip && Top == other.Top;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Skip, Top });
        }
    }
}
