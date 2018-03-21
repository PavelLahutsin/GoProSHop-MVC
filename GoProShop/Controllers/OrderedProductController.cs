using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderedProductController : Controller
    {
        private readonly IOrderedProductService _orderedProductService;
        private readonly IResponseService _responseService;

        public OrderedProductController(
           IOrderedProductService orderedProductService,
           IResponseService responseService)
        {
            _orderedProductService = orderedProductService ?? throw new ArgumentNullException(nameof(orderedProductService));
            _responseService = responseService ?? throw new ArgumentNullException(nameof(responseService));
        }

        public async Task<ActionResult> UpdateOrderedProducts(IEnumerable<int> productsId, int orderId)
        {
            await _orderedProductService.AddOrderedProductsAsync(productsId, orderId);

            return Json(_responseService.Create(true, string.Empty, Url.Action("GetOrderedProducts", new { id = orderId })), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrderedProducts(int id)
        {
            var orderedProducts = _orderedProductService.GetAll(x => x.ProductId == id);
            var orderedProductsVm = Mapper.Map<IEnumerable<OrderedProductDTO>, IEnumerable<OrderedProductVM>>(orderedProducts);

            return PartialView("_OrderedProducts", orderedProductsVm);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int orderedProductId)
        {
            var response = Mapper.Map<ResponseVM>(await _orderedProductService.DeleteAsync(orderedProductId, "Delete"));
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}