namespace Architecture3.Web
{
    using System.Web.Http;

    public static class RegisterRoutes
    {
        public static void Execute(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
