namespace Architecture3.Logic.Tests.CQ.TemplateMethods.Queries
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.TemplateMethods.Queries;
    using Architecture3.Logic.CQ.TemplateMethods.Queries.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class GetCommandHandlerTemplateTests
    {
        private IRepository _repository;
        private GetCommandHandlerTemplate<IQuery, IRepository, string> _handler;
        private IQuery _query;

        public interface IQuery : IId, IRequest<IResult<string, Error>>
        {
        }

        public interface IRepository : IGetRepository<string>
        {
        }

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository>();
            _query = Substitute.For<IQuery>();
            _handler = Substitute.For<GetCommandHandlerTemplate<IQuery, IRepository, string>>(_repository);
        }

        [Test]
        public void ShouldSucceed()
        {
            var id = (NonNegativeInt)1;
            const string value = "value";
            _query.Id.Returns(id);
            _repository.Get(id).Returns(value);
            var r = _handler.Handle(_query);
            r.IsSuccess.ShouldBeTrue();
            r.Value.ShouldBe(value);
        }

        [Test]
        public void NotExists_ShouldFail()
        {
            var id = (NonNegativeInt)1;
            const string value = null;
            _query.Id.Returns(id);
            _repository.Get(id).Returns(value);
            var r = _handler.Handle(_query);
            r.IsFailure.ShouldBeTrue();
            r.Error.ErrorType.ShouldBe(ErrorType.NotFound);
        }
    }
}
