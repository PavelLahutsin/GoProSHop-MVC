using GoProShop.BLL.DTO;
using GoProShop.DAL.Enums;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services
{
    public class FeedbackService : CrudService<Feedback, FeedbackDTO>, IFeedbackService
    {
         public FeedbackService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public new async Task CreateAsync(FeedbackDTO feedback)
        {
            feedback.Name = $"{feedback.Name.Trim()} {feedback.SurName.Trim()}";

            await base.CreateAsync(feedback);
        }

        public IEnumerable<FeedbackDTO> GetProductFeedbacks(int productId)
        {
            return GetAll(x => x.Status == FeedbackStatus.Approved && x.ProductId == productId).Reverse();
        }

        public IEnumerable<FeedbackDTO> GetFeedbacks() => GetAll().Reverse();

        public IEnumerable<FeedbackDTO> GetHomeFeedbacks()
        {
            return GetAll(x => x.Status == FeedbackStatus.Approved && x.ProductId == null).Reverse().Take(3);
        }

        public async Task<int> ViewFeedback(int id)
        {
            var feedback = await _uow.Repository<Feedback>().GetAsync(id);

            if (feedback?.IsViewed == false)
            {
                feedback.IsViewed = true;
                await _uow.Repository<Feedback>().UpdateAsync(feedback);
                await _uow.Commit();
            }

            return _uow.Repository<Feedback>().Entities.Count(x => !x.IsViewed);
        }
    }
}
