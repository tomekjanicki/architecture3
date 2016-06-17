﻿namespace Architecture3.WebApi
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using Architecture3.WebApi.Infrastructure;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public static class RegisterMiscs
    {
        public static void Execute(HttpConfiguration configuration)
        {
            configuration.Formatters.Clear();
            configuration.Formatters.Add(GetConfiguredJsonMediaTypeFormatter());
            configuration.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
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