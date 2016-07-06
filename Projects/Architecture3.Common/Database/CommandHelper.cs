namespace Architecture3.Common.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Types;
    using Dapper;

    public static class CommandHelper
    {
        private enum LikeType
        {
            Full,
            Left,
            Right
        }

        public static DataResult GetPagedFragment(TopSkip topSkip, string sort)
        {
            var dp = new DynamicParameters();
            dp.Add("SKIP", topSkip.Skip);
            dp.Add("TOP", topSkip.Top);
            return new DataResult((NonEmptyString)$@"{GetSort(sort)} OFFSET @SKIP ROWS FETCH NEXT @TOP ROWS ONLY", dp);
        }

        public static string GetSort(string sort)
        {
            return sort != string.Empty ? $@"ORDER BY {sort}" : string.Empty;
        }

        public static void SetValues(ICollection<NonEmptyString> criteria, DynamicParameters dp, DataResult like)
        {
            criteria.Add(like.Data);
            dp.AddDynamicParams(like.Parameters);
        }

        public static DataResult GetLikeCaluse(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Full);
        }

        public static DataResult GetLikeLeftCaluse(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Left);
        }

        public static DataResult GetLikeRightCaluse(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value)
        {
            return GetLikeCaluseInternal(fieldName, paramName, value, LikeType.Right);
        }

        public static WhereResult GetWhereStringWithParams(IReadOnlyCollection<NonEmptyString> criteria, DynamicParameters dp)
        {
            var where = criteria.Count == 0 ? string.Empty : $" WHERE {string.Join(" AND ", criteria)} ";
            return new WhereResult(where, dp);
        }

        public static NonEmptyString GetTranslatedSort(string modelColumn, NonEmptyString defaultSort, IReadOnlyCollection<NonEmptyString> allowedColumns)
        {
            if (modelColumn == string.Empty)
            {
                return defaultSort.ToUpperInvariant();
            }

            var arguments = modelColumn.Split(' ');
            if (arguments.Length != 2)
            {
                return defaultSort.ToUpperInvariant();
            }

            var ascendingResult = NonEmptyString.Create(arguments[1], (NonEmptyString)"Value");
            var columnResult = NonEmptyString.Create(arguments[0], (NonEmptyString)"Value");

            if (ascendingResult.IsFailure || columnResult.IsFailure)
            {
                return defaultSort.ToUpperInvariant();
            }

            var ascending = ascendingResult.Value.ToUpperInvariant() == "ASC";
            var column = columnResult.Value.ToUpperInvariant();
            return !allowedColumns.Select(c => c.ToUpperInvariant()).Contains(column) ? defaultSort.ToUpperInvariant() : (NonEmptyString)$"{column} {(@ascending ? "ASC" : "DESC")}";
        }

        private static DataResult GetLikeCaluseInternal(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value, LikeType likeType)
        {
            var escapeChar = (NonEmptyString)@"\";
            var dp = new DynamicParameters();
            dp.Add(paramName.Value.ToUpperInvariant(), ToLikeString(value, likeType, escapeChar));
            return new DataResult((NonEmptyString)$@"{fieldName.Value.ToUpperInvariant()} LIKE @{paramName.Value.ToUpperInvariant()} ESCAPE '{escapeChar}'", dp);
        }

        private static NonEmptyString ToLikeString(string input, LikeType likeType, NonEmptyString escapeChar)
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

        public class WhereResult
        {
            internal WhereResult(string @where, DynamicParameters parameters)
            {
                Where = @where;
                Parameters = parameters;
            }

            public string Where { get; }

            public DynamicParameters Parameters { get; }
        }

        public class DataResult
        {
            internal DataResult(NonEmptyString data, DynamicParameters parameters)
            {
                Data = data;
                Parameters = parameters;
            }

            public NonEmptyString Data { get; }

            public DynamicParameters Parameters { get; }
        }
    }
}