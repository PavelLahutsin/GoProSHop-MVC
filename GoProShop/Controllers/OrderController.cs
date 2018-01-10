
using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(OrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", model);
            }

            var session = Session["Cart"] as Cart;
           var order = await _orderService.CreateAsync(Mapper.Map<OrderDTO>(model), Mapper.Map<IEnumerable<CartItem>, IEnumerable<CartItemDTO>>(session.CartItems));
            session.Clear();


            return Json(new { });
        }
    }
}