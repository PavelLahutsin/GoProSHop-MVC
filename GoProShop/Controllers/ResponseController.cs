using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class ResponseController : Controller
    {
        public ActionResult GetSuccessResponse(string message)
        {
            ViewBag.Message = message;

            return PartialView("_SuccessResponse");
        }

        public ActionResult GetDangerResponse(string message)
        {
            ViewBag.Message = message;

            return PartialView("_DangerResponse");
        }

        // GET: Response
        public ActionResult Index()
        {
            return View();
        }
    }
}