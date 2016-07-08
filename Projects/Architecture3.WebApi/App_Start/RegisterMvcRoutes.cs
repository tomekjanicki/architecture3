namespace Architecture3.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RegisterMvcRoutes
    {
        public static void Execute()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}