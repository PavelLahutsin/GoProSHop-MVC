using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.DAL.Enums;
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
        private readonly IResponseService _responseService;

        public FeedbackService(IUnitOfWork uow, IResponseService responseService)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _responseService = responseService ?? throw new ArgumentNullException(nameof(responseService));
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

        public IEnumerable<FeedbackDTO> GetProductFeedbacks(int productId)
        {
            var feedbacks = _uow.Feedbacks.Entities.Where(x => x.Status == FeedbackStatus.Approved && x.ProductId == productId);
            var feedbacksDTO = Mapper.Map<IQueryable<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO.Reverse();
        }

        public IEnumerable<FeedbackDTO> GetFeedbacks()
        {
            var feedbacks = _uow.Feedbacks.Entities;
            var feedbacksDTO = Mapper.Map<IQueryable<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO.Reverse();
        }

        public IEnumerable<FeedbackDTO> GetHomeFeedbacks()
        {
            var feedbacks = _uow.Feedbacks.Entities.Where(x => x.Status == FeedbackStatus.Approved && x.ProductId == null);
            var feedbacksDTO = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO.Reverse().Take(3);
        }

        public async Task<ResponseDTO> DeleteAsync(int id)
        {
            var feedback = await _uow.Feedbacks.GetAsync(id);

            if (feedback == null)
                return _responseService.Create(false, "Данного отзыва не существует в базе данных");

            await _uow.Feedbacks.DeleteAsync(feedback);
            await _uow.Commit();

            return _responseService.Create(true, "Отзыв успешно удален из базы данных");
        }

        public async Task<int> ViewFeedback(int id)
        {
            var feedback = await _uow.Feedbacks.GetAsync(id);

            if (feedback?.IsViewed == false)
            {
                feedback.IsViewed = true;
                await _uow.Feedbacks.UpdateAsync(feedback);
                await _uow.Commit();
            }

            return _uow.Feedbacks.Entities.Where(x => !x.IsViewed)?.Count() ?? 0;
        }

        public FeedbackDTO GetLastFeedback()
        {
            var feedback = _uow.Feedbacks.Entities.ToList().LastOrDefault();
            var feedbackDTO = Mapper.Map<FeedbackDTO>(feedback);

            return feedbackDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(FeedbackDTO feedbackDTO)
        {
            var feedback = await _uow.Feedbacks.UpdateAsync(Mapper.Map<Feedback>(feedbackDTO));

            if (feedback == null)
                return _responseService.Create(false, "Данного отзыва не существует в базе данных");

            await _uow.Commit();

            return _responseService.Create(true, "Отзыв успешно обновлен в базе данных");
        }
    }
}
