using System;
using GoProShop.DAL.Interfaces;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.BLL.DTO;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using GoProShop.DAL.Entities;
using System.Data.Entity;
using System.Collections.Generic;

namespace GoProShop.BLL.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task<int> CreateAsync(OrderDTO orderDTO, IEnumerable<CartItemDTO> cartItems)
        {
            using (var transaction = _uow.Database.BeginTransaction())
            {
                try
                {
                    var order = Mapper.Map<Order>(orderDTO);
                    var user = await _uow.Repository<Customer>().Entities.FirstOrDefaultAsync(x => x.Email == orderDTO.Customer.Email);

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
                    _uow.Repository<Order>().Create(order);

                    await _uow.Commit();

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

        public async Task<ResponseDTO> CreateAsync(OrderDTO orderDto)
        {
            using (var transaction = _uow.Database.BeginTransaction())
            {
                try
                {
                    var order = Mapper.Map<Order>(orderDto);
                    var user = await _uow.Repository<Customer>().Entities.FirstOrDefaultAsync(x => x.Email == orderDto.Customer.Email);

                    if (user != null)
                    {
                        order.Customer = user;
                        order.CustomerId = user.Id;
                    }
                    order.TotalPrice = orderDto.OrdersList.Sum(x => x.Product.Price);

                    _uow.Repository<Order>().Create(order);
                    await _uow.Commit();
                    transaction.Commit();
                    return new ResponseDTO(true, "Заказ был успешно создан");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new ResponseDTO(true, "При создании заказа возникла ошибка");
                }
            }
        }

        public async Task<ResponseDTO> DeleteAsync(int id)
        {
            var order = await _uow.Repository<Order>().GetAsync(id);

            if (order == null)
                return new ResponseDTO(false, "Данного заказа не существует в базе данных");

            await _uow.Repository<Order>().DeleteAsync(order);
            await _uow.Commit();

            return new ResponseDTO(true, "Заказ успешно удален из базы данных");
        }

        public async Task<OrderDTO> GetAsync(int id)
        {
            var order = await _uow.Repository<Order>().GetAsync(id);
            var orderDto= Mapper.Map<OrderDTO>(order);

            return orderDto;
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var orders = _uow.Repository<Order>().Entities.ToList();
            var ordersDto = Mapper.Map<List<Order>, IEnumerable<OrderDTO>>(orders);

            return ordersDto.Reverse();
        }

        public async Task<int> ViewOrder(int id)
        {
            var order = await _uow.Repository<Order>().GetAsync(id);

            if (order?.IsViewed == false)
            {
                order.IsViewed = true;
                await _uow.Repository<Order>().UpdateAsync(order);
                await _uow.Commit();
            }

            return _uow.Repository<Order>().Entities.Count(x => !x.IsViewed);
        }

        public async Task<ResponseDTO> UpdateAsync(OrderDTO orderDto)
        {
            var orderedProducts = await _uow.Repository<OrderedProduct>().Entities.Where(x => x.OrderId == orderDto.Id).ToListAsync();
            var order = Mapper.Map<Order>(orderDto);
            order.OrdersList = orderedProducts;

            var result  = await _uow.Repository<Order>().UpdateAsync(order);

            if (result == null)
                return new ResponseDTO(false, "Данного заказа не существует в базе данных");

            await _uow.Commit();

            return new ResponseDTO(true, "Заказ успешно обновлен в базе данных");
        }
    }
}
