using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateAsync(OrderDTO order, IEnumerable<CartItemDTO> cartItems);

        Task<ResponseDTO> CreateAsync(OrderDTO orderDto);

        Task<OrderDTO> GetAsync(int id);

        IEnumerable<OrderDTO> GetOrders();

        Task<ResponseDTO> DeleteAsync(int id, string itemName);

        Task<int> ViewOrder(int id);

        Task<ResponseDTO> UpdateAsync(OrderDTO order);
    }
}
