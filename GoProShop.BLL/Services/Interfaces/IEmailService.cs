using System.Threading.Tasks;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendSuccessOrderEmail(int orderId);
    }
}
