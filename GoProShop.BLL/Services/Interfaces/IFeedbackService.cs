using GoProShop.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoProShop.DAL.Entities;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IFeedbackService : ICrudService<Feedback, FeedbackDTO>
    {
        IEnumerable<FeedbackDTO> GetHomeFeedbacks();

        IEnumerable<FeedbackDTO> GetFeedbacks();

        IEnumerable<FeedbackDTO> GetProductFeedbacks(int productId);

        Task<int> ViewFeedback(int id);
    }
}
