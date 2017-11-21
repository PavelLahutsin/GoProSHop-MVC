using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult PriceList()
        {
            return PartialView();
        }

        public ActionResult Stores()
        {
            return View();
        }
    }
}