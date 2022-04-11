using System.Web.Mvc;

namespace Library.Controllers
{
    public class MainController : LibraryController
    {
        public ActionResult Index()
        {
            ViewData["title"] = "Ana sayfa";
            return View();
        }
    }
}