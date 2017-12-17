using GoProShop.BLL.DTO;

namespace GoProShop.BLL.Services.Interfaces
{
    public class ResponseService : IResponseService
    {
        public ResponseDTO Create(bool isSuccess, string message)
        {
            return new ResponseDTO
            {
                IsSuccess = isSuccess,
                Message = message
            };
        }
    }
}
