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

        //Task<bool> IsFeedbackViewed(int id);

        Task<ResponseDTO> UpdateAsync(FeedbackDTO feedbackDTO);

        FeedbackDTO GetLastFeedback();

        Task<ResponseDTO> DeleteAsync(int id);

        Task<FeedbackDTO> GetAsync(int id);

        Task<int> ViewFeedback(int id);
    }
}
