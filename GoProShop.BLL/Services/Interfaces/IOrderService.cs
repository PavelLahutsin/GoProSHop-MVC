using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateAsync(OrderDTO order, IEnumerable<CartItemDTO> cartItems);

        Task<OrderDTO> GetAsync(int id);
    }
}
