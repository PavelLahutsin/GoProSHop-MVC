using AutoMapper;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
        }

        public ActionResult Index()
        {
            var adminPageDTO = _adminService.Build();
            var adminPageVM = Mapper.Map<AdminPageVM>(adminPageDTO);

            return View(adminPageVM);
        }

        public ActionResult Orders() => PartialView();

        public ActionResult Price() => PartialView();

        public ActionResult Stores() => View();

        public ActionResult Feedbacks() => PartialView("_Feedbacks");
    }
}