using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class ProductGroupController : Controller
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        // GET: ProductGroup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductGroups()
        {
            var productGroups =
                Mapper.Map<IEnumerable<ProductGroupDTO>, IEnumerable<ProductGroupVM>>(_productGroupService.GetGroups());
            return PartialView(productGroups);
        }
    }
}