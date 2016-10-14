namespace Architecture3.Common.Tests.Database
{
    using System.Collections.Generic;
    using Architecture3.Common.Database;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Dapper;
    using NUnit.Framework;
    using Shouldly;

    public class CommandHelperTests
    {
        [Test]
        public void GetPagedFragment_ShouldReturnExpectedValues()
        {
            var topSkip = GetValidTopSkip();

            const string sortField = "sf";

            var result = CommandHelper.GetPagedFragment(topSkip, sortField);

            result.Data.Value.ShouldBe("ORDER BY sf OFFSET @SKIP ROWS FETCH NEXT @TOP ROWS ONLY");

            result.Parameters.Get<int>(CommandHelper.TopParamName).ShouldBe(topSkip.Top.Value);

            result.Parameters.Get<int>(CommandHelper.SkipParamName).ShouldBe(topSkip.Skip.Value);
        }

        [Test]
        public void GetSort_ShouldReturnExpectedValues()
        {
            const string sortField = "sf";

            var result = CommandHelper.GetSort(sortField);

            result.ShouldBe("ORDER BY sf");
        }

        [Test]
        public void GetSort_Empty_ShouldReturnExpectedValues()
        {
            var result = CommandHelper.GetSort(string.Empty);

            result.ShouldBe(string.Empty);
        }

        [Test]
        public void SetValues_ShouldReturnExpectedValues()
        {
            const string p1 = "P1";
            var data = (NonEmptyString)"data";
            var criteria = new List<NonEmptyString>();
            var dp = new DynamicParameters();
            var idp = new DynamicParameters();
            idp.Add(p1, 1);
            var like = new CommandHelper.DataResult(data, idp);

            CommandHelper.SetValues(criteria, dp, like);

            criteria.ShouldContain(data);

            dp.ParameterNames.ShouldContain(p1);
        }

        [Test]
        public void GetLikeCaluse_ShouldReturnExpectedValues()
        {
            var fieldName = (NonEmptyString)"FN";
            var paramName = (NonEmptyString)"PN";
            var value = (NonEmptyString)"v";

            var result = CommandHelper.GetLikeCaluse(fieldName, paramName, value);

            result.Data.Value.ShouldBe(@"FN LIKE @PN ESCAPE '\'");

            result.Parameters.Get<string>(paramName).ShouldBe($"%{value}%");
        }

        [Test]
        public void GetLikeLeftCaluse_ShouldReturnExpectedValues()
        {
            var fieldName = (NonEmptyString)"FN";
            var paramName = (NonEmptyString)"PN";
            var value = (NonEmptyString)"v";

            var result = CommandHelper.GetLikeLeftCaluse(fieldName, paramName, value);

            result.Data.Value.ShouldBe(@"FN LIKE @PN ESCAPE '\'");

            result.Parameters.Get<string>(paramName).ShouldBe($"%{value}");
        }

        [Test]
        public void GetLikeRightCaluse_ShouldReturnExpectedValues()
        {
            var fieldName = (NonEmptyString)"FN";
            var paramName = (NonEmptyString)"PN";
            var value = (NonEmptyString)"v";

            var result = CommandHelper.GetLikeRightCaluse(fieldName, paramName, value);

            result.Data.Value.ShouldBe(@"FN LIKE @PN ESCAPE '\'");

            result.Parameters.Get<string>(paramName).ShouldBe($"{value}%");
        }

        private static TopSkip GetValidTopSkip()
        {
            var result = TopSkip.Create(20, 20, (NonEmptyString)nameof(TopSkip.Top), (NonEmptyString)nameof(TopSkip.Skip));
            result.EnsureIsNotFaliure();
            return result.Value;
        }
    }
}
