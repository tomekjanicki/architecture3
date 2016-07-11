namespace Architecture3.Common.Web
{
    using System.Web.Mvc;

    public abstract class PageController : Controller
    {
        protected virtual ActionResult View(IPageModel pageModel)
        {
            return View((object)pageModel);
        }
    }
}
