using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoProShop.BLL.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _uow;

        public FeedbackService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task CreateAsync(FeedbackDTO feedbackDTO)
        {
            feedbackDTO.Name = $"{feedbackDTO.Name.Trim()} {feedbackDTO.SurName.Trim()}";

            _uow.Feedbacks.Create(Mapper.Map<Feedback>(feedbackDTO));
            await _uow.Commit();
        }

        public async Task<FeedbackDTO> GetAsync(int id)
        {
            var feedback = await _uow.Feedbacks.GetAsync(id);
            var feedbackDTO = Mapper.Map<FeedbackDTO>(feedback);

            return feedbackDTO;
        }

        public IEnumerable<FeedbackDTO> GetFeedbacks()
        {
            var feedbacks = _uow.Feedbacks.Entities;
            var feedbacksDTO = Mapper.Map<IQueryable<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO.Reverse();
        }

        public IEnumerable<FeedbackDTO> GetHomeFeedbacks()
        {
            var feedbacks = _uow.Feedbacks.Entities.Where(x => x.IsApproved == true).Take(3).ToList();
            var feedbacksDTO = Mapper.Map<List<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO;
        }

        public async Task<int> ProcessFeedback(int id)
        {
            var feedback = await _uow.Feedbacks.GetAsync(id);

            if(feedback?.IsViewed == false)
            {
                feedback.IsViewed = true;
                await _uow.Feedbacks.UpdateAsync(feedback);
                await _uow.Commit();
            }

            return _uow.Feedbacks.Entities.Where(x => !x.IsViewed)?.Count() ?? 0;              
        }
    }
}
