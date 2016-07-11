namespace Architecture3.Web
{
    using System.Web.Http;
    using Architecture3.Common.Tools.Interfaces;
    using Swashbuckle.Application;

    public static class RegisterSwagger
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var provider = (IAssemblyVersionProvider)configuration.DependencyResolver.GetService(typeof(IAssemblyVersionProvider));
            var version = provider.Get(typeof(RegisterSwagger).Assembly);
            var versionString = $"{version.Major}_{version.Minor}";
            configuration.EnableSwagger(config => Configure(versionString, config)).EnableSwaggerUi(ConfigureUi);
        }

        private static void Configure(string version, SwaggerDocsConfig config)
        {
            config.SingleApiVersion(version, "Architecture3.Web");
            config.UseFullTypeNameInSchemaIds();
        }

        private static void ConfigureUi(SwaggerUiConfig config)
        {
            config.DocExpansion(DocExpansion.List);
            config.DisableValidator();
        }
    }
}