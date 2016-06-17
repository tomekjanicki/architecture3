namespace Architecture3.WebApi
{
    using System.Web.Http;

    public static class RegisterRoutes
    {
        public static void Execute(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
