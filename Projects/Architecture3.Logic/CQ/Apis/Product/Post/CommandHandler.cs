﻿namespace Architecture3.Logic.CQ.Apis.Product.Post
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Apis.Product.Post.Interfaces;
    using Architecture3.Logic.Shared;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class CommandHandler : IRequestHandler<Command, IResult<NonNegativeInt, Error>>
    {
        private readonly IRepository _repository;

        public CommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public IResult<NonNegativeInt, Error> Handle(Command message)
        {
            var codeExists = _repository.CodeExists(message.Code);

            if (codeExists)
            {
                return ((NonEmptyString)"Code already defined").ToGeneric<NonNegativeInt>();
            }

            var id = _repository.Insert(message);

            return Result<NonNegativeInt, Error>.Ok(id);
        }
    }
}
