using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class ProductSubGroupController : Controller
    {
        //private readonly IProductSubGroupService _productSubGroupService;

        //public ProductSubGroupController(IProductSubGroupService productSubGroupService)
        //{
        //    _productSubGroupService = productSubGroupService;
        //}

        //public ActionResult Index(int? id)
        //{
        //    return View();
        //}

        //public ActionResult ProductSubGroups()
        //{
        //    var productSubGroupsDTO = _productSubGroupService.GetProductSubGroups();
        //    var productSubGroupsVM = Mapper.Map<IEnumerable<ProductSubGroupDTO>,IEnumerable<ProductSubGroupVM>>(productSubGroupsDTO);

        //    return User.IsInRole("admin")
        //        ? PartialView("ProductSubGroups", productSubGroupsVM)
        //        : PartialView("_UserProductSubGroups", productSubGroupsVM);
        //}
    }
}