namespace Architecture3.WebApi.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

    public static class Helper
    {
        public static string GetEncodedParametersString(ICollection<Tuple<NonEmptyString, string>> parameters)
        {
            var array = parameters.Select(tuple => $"{Uri.EscapeDataString(tuple.Item1)}={Uri.EscapeDataString(tuple.Item2)}").ToArray();
            return array.Length == 0 ? string.Empty : $"?{string.Join("&", array)}";
        }

        public static HttpClient GetConfiguredHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static async Task<NonEmptyString> GetErrorMessage(HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return (NonEmptyString)$"Failed with {httpResponseMessage.StatusCode} {content}";
        }

        public static async Task<Result<T, NonEmptyString>> GetFailMessage<T>(HttpResponseMessage httpResponseMessage)
        {
            var message = await GetErrorMessage(httpResponseMessage).ConfigureAwait(false);
            return Result<T, NonEmptyString>.Fail(message);
        }

        public static async Task<Result<NonEmptyString>> GetFailMessage(HttpResponseMessage httpResponseMessage)
        {
            var message = await GetErrorMessage(httpResponseMessage).ConfigureAwait(false);
            return Result<NonEmptyString>.Fail(message);
        }

        public static Result<T, NonEmptyString> GetFailMessage<T>(NonEmptyString nonEmptyString)
        {
            return Result<T, NonEmptyString>.Fail(nonEmptyString);
        }

        public static Result<NonEmptyString> GetOkMessage()
        {
            return Result<NonEmptyString>.Ok();
        }

        public static Result<T, NonEmptyString> GetOkMessage<T>(T value)
        {
            return Result<T, NonEmptyString>.Ok(value);
        }
    }
}