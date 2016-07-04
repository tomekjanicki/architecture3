﻿namespace Architecture3.Logic.CQ.Product.Post
{
    using System.Linq;
    using Architecture3.Logic.CQ.Product.Post.Interfaces;
    using Architecture3.Logic.CQ.Product.ValueObjects;
    using Architecture3.Logic.Database.Interfaces;
    using Dapper;

    public class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool CodeExists(Code code)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.Query<bool>("X", new { code }).Single();
            }
        }

        public int Insert(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}