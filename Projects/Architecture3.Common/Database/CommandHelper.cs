namespace Architecture3.Common.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using Architecture3.Common.ValueObjects;
    using Dapper;

    public static class CommandHelper
    {
        private enum LikeType
        {
            Full,
            Left,
            Right
        }

        public static Result GetPagedFragment(TopSkip topSkip, string sort)
        {
            var dp = new DynamicParameters();
            dp.Add("SKIP", topSkip.Skip);
            dp.Add("TOP", topSkip.Top);
            return new Result($@"{GetSort(sort)} OFFSET @SKIP ROWS FETCH NEXT @TOP ROWS ONLY", dp);
        }

        public static string GetSort(string sort)
        {
            return sort != string.Empty ? $@"ORDER BY {sort}" : string.Empty;
        }

        public static void SetValues(ICollection<string> criteria, DynamicParameters dp, Result like)
        {
            criteria.Add(like.Query);
            dp.AddDynamicParams(like.Parameters);
        }

        public static Result GetLikeCaluse(string fieldName, string paramName, string value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Full);
        }

        public static Result GetLikeLeftCaluse(string fieldName, string paramName, string value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Left);
        }

        public static Result GetLikeRightCaluse(string fieldName, string paramName, string value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Right);
        }

        public static Result GetWhereStringWithParams(IReadOnlyCollection<string> criteria, DynamicParameters dp)
        {
            var where = criteria.Count == 0 ? string.Empty : $" WHERE {string.Join(" AND ", criteria)} ";
            return new Result(where, dp);
        }

        public static string GetTranslatedSort(string modelColumn, string defaultSort, IReadOnlyCollection<string> allowedColumns)
        {
            if (string.IsNullOrEmpty(modelColumn))
            {
                return defaultSort.ToUpperInvariant();
            }

            var arguments = modelColumn.Split(' ');
            if (arguments.Length != 2)
            {
                return defaultSort.ToUpperInvariant();
            }

            var ascending = arguments[1].ToUpperInvariant() == "ASC";
            var column = arguments[0].ToUpperInvariant();
            return !allowedColumns.Select(c => c.ToUpperInvariant()).Contains(column) ? defaultSort.ToUpperInvariant() : $"{column} {(@ascending ? "ASC" : "DESC")}";
        }

        private static Result GetLikeCaluseInternal(string fieldName, string paramName, string value, LikeType likeType)
        {
            const string escapeChar = @"\";
            var dp = new DynamicParameters();
            dp.Add(paramName.ToUpperInvariant(), ToLikeString(value, likeType, escapeChar));
            return new Result($@"{fieldName.ToUpperInvariant()} LIKE @{paramName.ToUpperInvariant()} ESCAPE '{escapeChar}'", dp);
        }

        private static string ToLikeString(string input, LikeType likeType, string escapeChar)
        {
            return
                likeType == LikeType.Right
                    ?
                    input.ToLikeRightString(escapeChar)
                    :
                    likeType == LikeType.Left
                        ?
                        input.ToLikeLeftString(escapeChar)
                        :
                        input.ToLikeString(escapeChar);
        }

        public class Result
        {
            public Result(string query, DynamicParameters parameters)
            {
                Query = query;
                Parameters = parameters;
            }

            public string Query { get; }

            public DynamicParameters Parameters { get; }
        }
    }
}