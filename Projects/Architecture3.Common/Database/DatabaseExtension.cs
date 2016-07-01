namespace Architecture3.Common.Database
{
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;

    public static class DatabaseExtension
    {
        public static IDbConnection GetConnection(string key)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[key];
            var factory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            var connection = factory.CreateConnection();
            Debug.Assert(connection != null, $"{nameof(connection)} != null");
            connection.ConnectionString = connectionString.ConnectionString;
            connection.Open();
            return connection;
        }

        public static string ToLikeString(this string input, string escapeChar)
        {
            return input == null ? null : $"%{input.ToLikeStringInternal(escapeChar)}%";
        }

        public static string ToLikeLeftString(this string input, string escapeChar)
        {
            return input == null ? null : $"%{input.ToLikeStringInternal(escapeChar)}";
        }

        public static string ToLikeRightString(this string input, string escapeChar)
        {
            return input == null ? null : $"{input.ToLikeStringInternal(escapeChar)}%";
        }

        private static string ToLikeStringInternal(this string input, string escapeChar)
        {
            input = input.Replace(escapeChar, string.Format("{0}{0}", escapeChar));
            input = input.Replace("%", $"{escapeChar}%");
            input = input.Replace("_", $"{escapeChar}_");
            input = input.Replace("[", $"{escapeChar}[");
            return input;
        }
    }
}
