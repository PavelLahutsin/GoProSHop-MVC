using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;
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
            var orderDTO = await _orderService.CreateAsync(Mapper.Map<OrderDTO>(model),
                Mapper.Map<IEnumerable<CartItem>, IEnumerable<CartItemDTO>>(session.CartItems));
            session.Clear();

            await _emailService.SendSuccessOrderEmail(orderDTO.Id);

            return Json(_responseService.Create(true, string.Empty, Url.Action("SuccessOrder", "Order", new { id = orderDTO.Id })));
        }
    }
}