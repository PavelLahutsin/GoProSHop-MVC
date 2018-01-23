using System;
using GoProShop.DAL.Interfaces;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.BLL.DTO;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq;
using AutoMapper;
using GoProShop.DAL.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace GoProShop.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uof;

        public OrderService(IUnitOfWork uof)
        {
            _uof = uof ?? throw new ArgumentNullException(nameof(uof));
        }

        public async Task<int> CreateAsync(OrderDTO orderDTO, IEnumerable<CartItemDTO> cartItems)
        {
            using (var transaction = _uof.Database.BeginTransaction())
            {
                try
                {
                    var order = Mapper.Map<Order>(orderDTO);
                    var user = await _uof.Customers.Entities.FirstOrDefaultAsync(x => x.Email == orderDTO.Customer.Email);

                    if (user != null)
                    {
                        order.Customer = user;
                        order.CustomerId = user.Id;
                    }

                    order.TotalPrice = cartItems.Sum(x => x.Product.Price);
                    order.Condition = DAL.Enums.Condition.Awaiting;
                    order.OrdersList = cartItems.Select(x => new OrderedProduct
                    {
                        OrderId = order.Id,
                        ProductId = x.Product.Id,
                        Price = x.Product.Price,
                        Quantity = x.Quantity
                    }).ToList();
                    _uof.Orders.Create(order);

                    await _uof.Commit();

                    transaction.Commit();

                    return order.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return default(int);
                }
            }
        }

        public async Task<ResponseDTO> DeleteAsync(int id)
        {
            var order = await _uof.Orders.GetAsync(id);

            if (order == null)
                return new ResponseDTO(false, "Данного отзыва не существует в базе данных");

            await _uof.Orders.DeleteAsync(order);
            await _uof.Commit();

            return new ResponseDTO(true, "Отзыв успешно удален из базы данных");
        }

        public async Task<OrderDTO> GetAsync(int id)
        {
            var order = await _uof.Orders.GetAsync(id);
            var orderDTO = Mapper.Map<OrderDTO>(order);

            return orderDTO;
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var orders = _uof.Orders.Entities.ToList();
            var ordersDTO = Mapper.Map<List<Order>, IEnumerable<OrderDTO>>(orders);

            return ordersDTO.Reverse();
        }

        public async Task<int> ViewOrder(int id)
        {
            var order = await _uof.Orders.GetAsync(id);

            if (order?.IsViewed == false)
            {
                order.IsViewed = true;
                await _uof.Orders.UpdateAsync(order);
                await _uof.Commit();
            }

            return _uof.Orders.Entities.Where(x => !x.IsViewed)?.Count() ?? 0;
        }
    }
}
