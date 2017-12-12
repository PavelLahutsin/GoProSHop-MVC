using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class ProductGroupController : Controller
    {
        private readonly IProductGroupService _mainProductGroupService;

        public ProductGroupController(IProductGroupService mainProductGroupService)
        {
            _mainProductGroupService = mainProductGroupService ?? throw new ArgumentNullException(nameof(mainProductGroupService));
        }

        public ActionResult MegaMenu()
        {
            var productGroupsDTO = _mainProductGroupService.GetProductGroups();
            var productGroupsVM =
                Mapper.Map<IEnumerable<ProductGroupDTO>, IEnumerable<ProductGroupVM>>(productGroupsDTO);

            return PartialView("_MegaMenu", productGroupsVM);
        }

        public ActionResult SideMenu()
        {
            var productGroupsDTO = _mainProductGroupService.GetProductGroups();
            var productGroupsVM =
                Mapper.Map<IEnumerable<ProductGroupDTO>, IEnumerable<ProductGroupVM>>(productGroupsDTO);

            return PartialView("_SideMenu", productGroupsVM);
        }
    }
}