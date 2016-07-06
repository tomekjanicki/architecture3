namespace Architecture3.Logic.Tests.CQ.Product.Delete
{
    using Architecture3.Logic.CQ.Product.Delete;
    using NUnit.Framework;
    using Shouldly;

    public class CommandTests
    {
        [Test]
        public void ValidParameters_ShouldPass()
        {
            var commandResult = Command.Create(1, "X");
            commandResult.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void InvalidId_ShouldFail()
        {
            var commandResult = Command.Create(-1, "X");
            commandResult.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void InvalidVersion_ShouldFail()
        {
            var commandResult = Command.Create(1, string.Empty);
            commandResult.IsFailure.ShouldBeTrue();
        }
    }
}
