using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IOrderedProductService
    {
        Task AddOrderedProductsAsync(IEnumerable<int> productsId, int orderId);

        IEnumerable<OrderedProductDTO> GetOrderedProducts(int orderId);

        Task<ResponseDTO> DeleteAsync(int id, string itemName);
    }
}
