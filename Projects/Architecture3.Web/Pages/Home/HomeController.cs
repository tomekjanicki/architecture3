namespace Architecture3.Web.Pages.Home
{
    using System.Web.Mvc;
    using Architecture3.Common.Web;

    public class HomeController : PageController
    {
        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }
    }
}