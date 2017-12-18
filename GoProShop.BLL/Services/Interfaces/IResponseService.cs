using GoProShop.BLL.DTO;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IResponseService
    {
        ResponseDTO Create(bool isSuccess, string message);
    }
}
