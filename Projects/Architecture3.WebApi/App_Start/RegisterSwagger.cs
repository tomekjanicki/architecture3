namespace Architecture3.WebApi
{
    using System.Web.Http;
    using Swashbuckle.Application;

    public static class RegisterSwagger
    {
        public static void Execute(HttpConfiguration configuration)
        {
            configuration.EnableSwagger(Configure).EnableSwaggerUi(ConfigureUi);
        }

        private static void Configure(SwaggerDocsConfig config)
        {
            config.SingleApiVersion("v1", "Architecture3.WebApi");
        }

        private static void ConfigureUi(SwaggerUiConfig config)
        {
            config.DocExpansion(DocExpansion.List);
            config.DisableValidator();
        }
    }
}