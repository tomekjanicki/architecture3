namespace Architecture3.Web
{
    using System.Web.Mvc;

    public static class RegisterMvcMiscs
    {
        public static void Execute()
        {
            var viewLocations = new[] { "~/Pages/Shared/Views/{0}.cshtml", "~/Pages/{1}/{0}.cshtml" };
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine
            {
                ViewLocationFormats = viewLocations,
                PartialViewLocationFormats = viewLocations,
                MasterLocationFormats = viewLocations
            });
        }
    }
}