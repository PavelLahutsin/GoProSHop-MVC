using GoProShop.BLL.DTO;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> Create(OrderDTO order);
    }
}
