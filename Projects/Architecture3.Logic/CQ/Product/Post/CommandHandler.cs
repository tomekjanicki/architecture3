﻿namespace Architecture3.Logic.CQ.Product.Post
{
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Logic.CQ.Product.Post.Interfaces;
    using Architecture3.Logic.Facades.Shared;
    using Architecture3.Types.FunctionalExtensions;

    public sealed class CommandHandler : IRequestHandler<Command, Result<int, Error>>
    {
        private readonly IRepository _repository;

        public CommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Result<int, Error> Handle(Command message)
        {
            var codeExists = _repository.CodeExists(message.Code);
            if (codeExists)
            {
                return Result<int, Error>.Fail(Error.CreateBadRequest("Code already defined"));
            }

            var id = _repository.Insert(message);

            return Result<int, Error>.Ok(id);
        }
    }
}