using System;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System.Transactions;

namespace GoProShop.BLL.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _uof;

        public OrderService(IUnitOfWork uof)
        {
            _uof = uof ?? throw new ArgumentNullException(nameof(uof));
        }

        public void Create(Order order)
        {
           if(order == null)
                throw new ArgumentNullException(nameof(order));

          //TODO : implement create new order logic
        }
    }
}
