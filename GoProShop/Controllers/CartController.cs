using AutoMapper;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<ActionResult> AddToCart(int id)
        {
            var productDTO = await _productService.GetAsync(id);

            if (productDTO == null)
                return HttpNotFound();

            var productVM = Mapper.Map<ProductVM>(productDTO);
            GetCart.Add(productVM);

            return Json(new
            {
                Quantity = GetCart.Count
            }, 
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveCartItem(int id)
        {
            GetCart.Remove(id);
            return RedirectToAction("CartView");
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