using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IFeedbackService
    {
        IEnumerable<FeedbackDTO> GetHomeFeedbacks();

        Task CreateAsync(FeedbackDTO feedbackDTO);

        IEnumerable<FeedbackDTO> GetFeedbacks();

        IEnumerable<FeedbackDTO> GetProductFeedbacks(int productId);

        Task<ResponseDTO> UpdateAsync(FeedbackDTO feedbackDTO, string itemName);

        Task<ResponseDTO> DeleteAsync(int id, string itemName);

        Task<FeedbackDTO> GetAsync(int id);

        Task<int> ViewFeedback(int id);
    }
}
