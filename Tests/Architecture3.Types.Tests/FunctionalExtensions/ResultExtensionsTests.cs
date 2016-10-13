namespace Architecture3.Types.Tests.FunctionalExtensions
{
    using Architecture3.Types.FunctionalExtensions;
    using NUnit.Framework;
    using Shouldly;

    public class ResultExtensionsTests
    {
        [Test]
        public void IfAtLeastOneFailCombineElseReturnOk_ShouldFail()
        {
            var r1 = ((NonEmptyString)"e1").GetFailResult();
            var r2 = ResultExtensions.GetOkMessage();

            var result = new[] { r1, r2 }.IfAtLeastOneFailCombineElseReturnOk();

            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void IfAtLeastOneFailCombineElseReturnOk_ShouldSucceed()
        {
            var r1 = ResultExtensions.GetOkMessage();
            var r2 = ResultExtensions.GetOkMessage();

            var result = new[] { r1, r2 }.IfAtLeastOneFailCombineElseReturnOk();

            result.IsSuccess.ShouldBeTrue();
        }
    }
}
