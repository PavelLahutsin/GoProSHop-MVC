
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }
    }
}