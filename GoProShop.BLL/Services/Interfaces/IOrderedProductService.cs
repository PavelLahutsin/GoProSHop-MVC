using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IOrderedProductService : ICrudService<OrderedProduct, OrderedProductDTO>
    {
        Task AddOrderedProductsAsync(IEnumerable<int> productsId, int orderId);
    }
}
