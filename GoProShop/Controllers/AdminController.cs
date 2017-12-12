using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult Orders() => View();

        public ActionResult Price() => PartialView();

        public ActionResult Stores() => View();
    }
}