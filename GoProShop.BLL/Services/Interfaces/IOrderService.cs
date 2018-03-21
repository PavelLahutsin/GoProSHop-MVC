using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IOrderService : ICrudService<Order, OrderDTO>
    {
        Task<int> CreateAsync(OrderDTO order, IEnumerable<CartItemDTO> cartItems);

        IEnumerable<OrderDTO> GetOrders();

        Task<int> ViewOrder(int id);

        Task<ResponseDTO> UpdateAsync(OrderDTO order);
    }
}
