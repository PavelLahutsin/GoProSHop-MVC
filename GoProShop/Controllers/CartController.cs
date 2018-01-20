using AutoMapper;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IResponseService _responseService;

        public CartController(IProductService productService,
            IResponseService responseService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _responseService = responseService ?? throw new ArgumentNullException(nameof(responseService));
        }

        public async Task<ActionResult> AddToCart(int id)
        {
            var productDTO = await _productService.GetAsync(id);

            if (productDTO == null)
                return HttpNotFound();

            var productVM = Mapper.Map<ProductVM>(productDTO);
            GetCart.Add(productVM);

            Thread.Sleep(400);
            return Json(new
            {
                IsSuccess = true,
                Quantity = GetCart.Count
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReduceCartItem(int id)
        {
            GetCart.Reduce(id);
            var responseDTO = _responseService.Create(true, "");
            return Json(Mapper.Map<ResponseVM>(responseDTO), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveCartItem(int id)
        {
            GetCart.Remove(id);
            return Json(new { Quantity = GetCart.Count}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckOut()
        {
            return View(GetCart);
        }

        public ActionResult OrderInfo()
        {
            return PartialView("_OrderInfo", GetCart);
        }

        public ActionResult CartView()
        {
            return PartialView("_CartView", GetCart);
        }

        public ActionResult Index()
        {
            return PartialView("_Index", GetCart);
        }

        private Cart GetCart
        {
            get
            {
                Cart cart = Session["Cart"] as Cart;

                if (cart == null)
                {
                    cart = new Cart();
                    Session["Cart"] = cart;
                }

                return cart;
            }
        }
    }
}