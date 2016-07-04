﻿namespace Architecture3.Logic.CQ.Product
{
    using System;
    using System.Linq;
    using Architecture3.Logic.Database.Interfaces;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Dapper;

    public class SharedQueries
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public SharedQueries(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool ExistsById(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.Query<bool>("x", new { id }).Single();
            }
        }

        public Maybe<string> GetRowVersionById(NonNegativeInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var result = connection.Query<byte[]>("SELECT VERSION FROM DBO.PRODUCTS WHERE ID = @ID", new { id }).SingleOrDefault();
                return result != null ? Convert.ToBase64String(result) : null;
            }
        }
    }
}
