﻿namespace Architecture3.Web
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using Architecture3.Web.Infrastructure;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public static class RegisterWebApiMiscs
    {
        public static void Execute(HttpConfiguration configuration)
        {
            configuration.Formatters.Clear();
            configuration.Formatters.Add(GetConfiguredJsonMediaTypeFormatter());
            configuration.Services.Add(typeof(IExceptionLogger), new GlobalWebApiExceptionLogger());
        }

        private static JsonMediaTypeFormatter GetConfiguredJsonMediaTypeFormatter()
        {
            var result = new JsonMediaTypeFormatter();
            var mediaTypeHeaderValue = result.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "text/json");
            if (mediaTypeHeaderValue != null)
            {
                result.SupportedMediaTypes.Remove(mediaTypeHeaderValue);
            }

            result.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            result.SerializerSettings.Converters.Add(new StringEnumConverter());
            return result;
        }
    }
}