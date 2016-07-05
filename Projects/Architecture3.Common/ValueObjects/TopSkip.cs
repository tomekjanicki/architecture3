﻿namespace Architecture3.Common.ValueObjects
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

        public static Result<TopSkip, string> Create(int skip, int top, NonEmptyString topField, NonEmptyString skipField)
        {
            var skipResult = NonNegativeInt.Create(skip, skipField);

            var topResult = GreaterThanZeroInt.Create(top, topField);

            var result = ResultExtensions.CombineFailures(new IResult<string>[]
            {
                skipResult,
                topResult
            });

            if (result.IsFailure)
            {
                return GetFailResult((NonEmptyString)result.Error);
            }

            return topResult.Value > Const.MaxTopSize ? GetFailResult((NonEmptyString)$"{{0}} can't be greater than {Const.MaxTopSize}", topField) : GetOkResult(new TopSkip(skipResult.Value, topResult.Value));
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
