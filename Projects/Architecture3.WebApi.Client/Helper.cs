namespace Architecture3.WebApi.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public static class Helper
    {
        public static string GetEncodedParametersString(ICollection<Tuple<string, string>> parameters)
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
    }
}