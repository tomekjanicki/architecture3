namespace Architecture3.Common.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using Architecture3.Common.ValueObjects;
    using Architecture3.Types;
    using Dapper;

    public static class CommandHelper
    {
        public const string TopParamName = "TOP";
        public const string SkipParamName = "SKIP";

        private enum LikeType
        {
            Full,
            Left,
            Right
        }

        public static DataResult GetPagedFragment(TopSkip topSkip, string sort)
        {
            var dp = new DynamicParameters();
            dp.Add(SkipParamName, topSkip.Skip.Value);
            dp.Add(TopParamName, topSkip.Top.Value);
            return new DataResult((NonEmptyString)$@"{GetSort(sort)} OFFSET @{SkipParamName} ROWS FETCH NEXT @{TopParamName} ROWS ONLY", dp);
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

        public static NonEmptyString GetTranslatedSort(IReadOnlyCollection<OrderBy> modelOrderBy, OrderBy defaultDatabaseOrderBy, IReadOnlyDictionary<NonEmptyString, NonEmptyString> modelDatabaseColumnMappings)
        {
            if (modelOrderBy.Count == 0)
            {
                return GetSortColumn(defaultDatabaseOrderBy.Column, defaultDatabaseOrderBy.Order);
            }

            var dictionaryWithLowerCaseKeys = modelDatabaseColumnMappings.ToDictionary(pair => (NonEmptyString)pair.Key.Value.ToLower(), pair => pair.Value);

            var result = new List<NonEmptyString>();

            foreach (var orderBy in modelOrderBy)
            {
                var key = (NonEmptyString)orderBy.Column.Value.ToLower();

                if (dictionaryWithLowerCaseKeys.ContainsKey(key))
                {
                    result.Add(GetSortColumn(dictionaryWithLowerCaseKeys[key], orderBy.Order));
                }
            }

            if (result.Count == 0)
            {
                return GetSortColumn(defaultDatabaseOrderBy.Column, defaultDatabaseOrderBy.Order);
            }

            return (NonEmptyString)string.Join(", ", result);
        }

        private static NonEmptyString GetSortColumn(NonEmptyString column, bool order)
        {
            return (NonEmptyString)(column.Value.ToUpper() + " " + (order ? "ASC" : "DESC"));
        }

        private static DataResult GetLikeCaluseInternal(NonEmptyString fieldName, NonEmptyString paramName, NonEmptyString value, LikeType likeType)
        {
            var escapeChar = (NonEmptyString)@"\";
            var dp = new DynamicParameters();
            dp.Add(paramName.Value.ToUpperInvariant(), ToLikeString(value, likeType, escapeChar).Value);
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
            public DataResult(NonEmptyString data, DynamicParameters parameters)
            {
                Data = data;
                Parameters = parameters;
            }

            public NonEmptyString Data { get; }

            public DynamicParameters Parameters { get; }
        }
    }
}