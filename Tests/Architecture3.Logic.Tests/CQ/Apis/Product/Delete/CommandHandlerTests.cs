namespace Architecture3.Logic.Tests.CQ.Apis.Product.Delete
{
    using Architecture3.Logic.CQ.Apis.Product.Delete;
    using Architecture3.Logic.CQ.Apis.Product.Delete.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class CommandHandlerTests
    {
        private CommandHandler _commandHandler;
        private IRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository>();
            _commandHandler = new CommandHandler(_repository);
        }

        [Test]
        public void CantDelete_ShouldBeFaliure()
        {
            var command = GetValidCommand();
            _repository.ExistsById(Arg.Any<NonNegativeInt>()).Returns(true);
            _repository.GetRowVersionById(Arg.Any<NonNegativeInt>()).Returns(command.IdVersion.Version);
            _repository.CanBeDeleted(Arg.Any<NonNegativeInt>()).Returns(false);

            var result = _commandHandler.Handle(command);

            result.IsFailure.ShouldBeTrue();
            result.Error.ErrorType.ShouldBe(ErrorType.Generic);
        }

        private static Command GetValidCommand()
        {
            var commandResult = Command.Create(1, "x");
            commandResult.EnsureIsNotFaliure();
            return commandResult.Value;
        }
    }
}
