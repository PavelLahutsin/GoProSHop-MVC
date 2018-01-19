using GoProShop.BLL.DTO;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendSuccessOrderEmail(OrderDTO order);
    }
}
