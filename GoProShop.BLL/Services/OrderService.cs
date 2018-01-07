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
using System.Collections;
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

        public async Task<OrderDTO> Create(OrderDTO orderDTO)
        {
            using (var transaction = _uof.Database.BeginTransaction())
            {
                try
                {
                    var user = await _uof.Customers.Entities.FirstOrDefaultAsync(x => x.Email == orderDTO.Customer.Email);

                    if(user == null)
                    {
                        var mappedUser = Mapper.Map<Customer>(orderDTO.Customer);
                        user = _uof.Customers.Create(mappedUser);
                        await _uof.Commit();
                    }

                    var order = Mapper.Map<Order>(orderDTO);
                    _uof.Orders.Create(order);
                    await _uof.Commit();

                    var productsOrdered = Mapper.Map<IEnumerable<OrderedProductDTO>, IEnumerable<OrderedProduct>>(orderDTO.OrdersList);
                    _uof.ProductsOrdered.Entities.AddRange(productsOrdered);
                    await _uof.Commit();

                    transaction.Commit();

                    return Mapper.Map<OrderDTO>(order);
                }
                catch(Exception)
                {
                    transaction.Rollback();
                    return null;
                }
            }   
        }
    }
}
