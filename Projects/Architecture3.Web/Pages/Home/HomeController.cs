namespace Architecture3.Web.Pages.Home
{
    using System.Web.Mvc;
    using Architecture3.Logic.Facades.Pages;
    using Architecture3.Web.Infrastructure;

    public class HomeController : BaseMvcPageController
    {
        private readonly HomeIndexFacade _homeIndexFacade;

        public HomeController(HomeIndexFacade homeIndexFacade)
        {
            _homeIndexFacade = homeIndexFacade;
        }

        public ActionResult Index()
        {
            return View(_homeIndexFacade.GetViewModel());
        }
    }
}