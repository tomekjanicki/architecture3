namespace Architecture3.Web.Infrastructure
{
    using System.Web.Mvc;
    using Architecture3.Web.Dtos;

    public abstract class BaseMvcPageController : Controller
    {
        protected virtual ActionResult View(IPageModel pageModel)
        {
            return View((object)pageModel);
        }
    }
}
