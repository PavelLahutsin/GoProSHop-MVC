namespace GoProShop.BLL.DTO
{
    public class ResponseDTO
    {
        public ResponseDTO()
        {
        }

        public ResponseDTO(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public ResponseDTO(bool isSuccess, string message, string url)
            :this(isSuccess,message)
        {
            Url = url;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string Url { get; set; }
    }
}
