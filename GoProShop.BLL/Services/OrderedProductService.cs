using System.Collections.Generic;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Entities;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services
{
    public class OrderedProductService : CrudService<OrderedProduct, OrderedProductDTO>, IOrderedProductService
    {
        public OrderedProductService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task AddOrderedProductsAsync(IEnumerable<int> productsId, int orderId)
        {
            foreach (var productId in productsId)
            {
                var price = _uow.Repository<Product>().Entities.FirstOrDefault(x => x.Id == productId)?.Price;

                var productsOrdered = new OrderedProduct
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Quantity = 1,
                    Price = price ?? 0
                };

                _uow.Repository<OrderedProduct>().Create(productsOrdered);
            }
                
            await _uow.Commit();    
        }

        public IEnumerable<OrderedProductDTO> GetOrderedProducts(int orderId) => GetAll(x => x.OrderId == orderId);
    }
}
