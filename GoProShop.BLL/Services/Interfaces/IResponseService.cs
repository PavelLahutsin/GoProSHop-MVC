using GoProShop.BLL.DTO;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IResponseService
    {
        ResponseDTO Create(bool isSuccess, string message, string url);
        ResponseDTO Create(bool isSuccess, string message);
        ResponseDTO Create();
    }
}
