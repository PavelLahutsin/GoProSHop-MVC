using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoProShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;
        private readonly IResponseService _responseService;

        public OrderController(
            IOrderService orderService,
            IEmailService emailService,
            IResponseService responseService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _responseService = responseService ?? throw new ArgumentNullException(nameof(responseService));
        }

        [Authorize(Roles = "admin")]
        public ActionResult GetAdminOrders(int page = 1)
        {
            var ordersDTO = _orderService.GetOrders();
            var ordersVM = Mapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderVM>>(ordersDTO);

            return PartialView("_AdminOrders", ordersVM.ToPagedList(page, 10));
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        public async Task<ActionResult> SuccessOrder(int id)
        {
            var orderDTO = await _orderService.GetAsync(id);
            var orderVM = Mapper.Map<OrderVM>(orderDTO);

            return View("~/Views/Order/SuccessOrder.cshtml", orderVM);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> View(int id)
        {
            var pendingFeedbackCount = await _orderService.ViewOrder(id);

            return Json(new { Count = pendingFeedbackCount }, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(OrderVM model)
        {
            if (!ModelState.IsValid)
            {
                Thread.Sleep(400);
                return PartialView("_Create", model);
            }

            var session = Session["Cart"] as Cart;
            var orderId = await _orderService.CreateAsync(Mapper.Map<OrderDTO>(model),
                Mapper.Map<IEnumerable<CartItem>, IEnumerable<CartItemDTO>>(session.CartItems));
            session.Clear();

            await _emailService.SendSuccessOrderEmail(orderId);

            return Json(_responseService.Create(true, string.Empty, Url.Action("SuccessOrder", "Order", new { id = orderId })));
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateAdmin()
        {
            return PartialView("_CreateAdmin");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateAdmin(OrderVM model)
        {
            if (!ModelState.IsValid)
            {
                Thread.Sleep(400);
                return PartialView("_Create", model);
            }

            var response = await _orderService.CreateAsync(Mapper.Map<OrderDTO>(model));

            return Json(response);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var orderDto = await _orderService.GetAsync(id);
            return PartialView("_Edit", Mapper.Map<OrderVM>(orderDto));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", model);
            }

            var result = await _orderService.UpdateAsync(Mapper.Map<OrderDTO>(model));

            return Json(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = Mapper.Map<ResponseVM>(await _orderService.DeleteAsync(id));
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}