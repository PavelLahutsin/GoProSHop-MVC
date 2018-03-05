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
    public class OrderedProductService : BaseService, IOrderedProductService
    {
        public OrderedProductService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task AddOrderedProductsAsync(IEnumerable<int> productsId, int orderId)
        {
            foreach (var productId in productsId)
            {
                var price = _uow.Repository<Product>().Entities.FirstOrDefault(x => x.Id == productId).Price;

                var productsOrdered = new OrderedProduct
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Quantity = 1,
                    Price = price
                };

                _uow.Repository<OrderedProduct>().Create(productsOrdered);
            }
                
            await _uow.Commit();    
        }

        public async Task<ResponseDTO> DeleteAsync(int id)
        {
            var orderedProduct = await _uow.Repository<OrderedProduct>().GetAsync(id);

            if (orderedProduct == null)
                return new ResponseDTO(false, "Товар не содержится в заказе");

            await _uow.Repository<OrderedProduct>().DeleteAsync(orderedProduct);
            await _uow.Commit();

            return new ResponseDTO(true, "Товар успешно удален из заказа");
        }

        public IEnumerable<OrderedProductDTO> GetOrderedProducts(int orderId)
        {
            var orderedProducts = _uow.Repository<OrderedProduct>().Entities.Where(x => x.OrderId == orderId).ToList();

            var orderedProductsDto = Mapper.Map<List<OrderedProduct>, IEnumerable<OrderedProductDTO>>(orderedProducts);

            return orderedProductsDto;
        }
    }
}
