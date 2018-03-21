using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;

namespace GoProShop.BLL.Services
{
    public class ResponseService : IResponseService
    {
        public ResponseDTO Create(bool isSuccess, string message, string url)
        {
            return new ResponseDTO
            {
                IsSuccess = isSuccess,
                Message = message,
                Url = url
            };
        }

        public ResponseDTO Create(bool isSuccess, string message)
        {
            return new ResponseDTO
            {
                IsSuccess = isSuccess,
                Message = message,
            };
        }

        public ResponseDTO Create()
        {
            return new ResponseDTO
            {
                IsSuccess = false,
                Message = string.Empty,
            };
        }
    }
}
