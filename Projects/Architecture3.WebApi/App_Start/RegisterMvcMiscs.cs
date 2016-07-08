namespace Architecture3.Web
{
    using System.Web.Mvc;

    public static class RegisterMvcMiscs
    {
        public static void Execute()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}