using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class CatalogController : Controller
    {
        public ActionResult Index(int? id)
        {
            return View();
        }
    }
}