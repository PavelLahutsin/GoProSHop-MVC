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
    public class FeedbackService : BaseService, IFeedbackService
    {
         public FeedbackService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task CreateAsync(FeedbackDTO feedbackDTO)
        {
            feedbackDTO.Name = $"{feedbackDTO.Name.Trim()} {feedbackDTO.SurName.Trim()}";

            _uow.Repository<Feedback>().Create(Mapper.Map<Feedback>(feedbackDTO));
            await _uow.Commit();
        }

        public async Task<FeedbackDTO> GetAsync(int id)
        {
            var feedback = await _uow.Repository<Feedback>().GetAsync(id);
            var feedbackDTO = Mapper.Map<FeedbackDTO>(feedback);

            return feedbackDTO;
        }

        public IEnumerable<FeedbackDTO> GetProductFeedbacks(int productId)
        {
            var feedbacks = _uow.Repository<Feedback>().Entities.Where(x => x.Status == FeedbackStatus.Approved && x.ProductId == productId);
            var feedbacksDTO = Mapper.Map<IQueryable<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO.Reverse();
        }

        public IEnumerable<FeedbackDTO> GetFeedbacks()
        {
            var feedbacks = _uow.Repository<Feedback>().Entities;
            var feedbacksDTO = Mapper.Map<IQueryable<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO.Reverse();
        }

        public IEnumerable<FeedbackDTO> GetHomeFeedbacks()
        {
            var feedbacks = _uow.Repository<Feedback>().Entities.Where(x => x.Status == FeedbackStatus.Approved && x.ProductId == null);
            var feedbacksDTO = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO.Reverse().Take(3);
        }

        public async Task<ResponseDTO> DeleteAsync(int id)
        {
            var feedback = await _uow.Repository<Feedback>().GetAsync(id);

            if (feedback == null)
                return new ResponseDTO(false, "Данного отзыва не существует в базе данных");

            await _uow.Repository<Feedback>().DeleteAsync(feedback);
            await _uow.Commit();

            return new ResponseDTO(true, "Отзыв успешно удален из базы данных");
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

            return _uow.Repository<Feedback>().Entities.Where(x => !x.IsViewed)?.Count() ?? 0;
        }

        public FeedbackDTO GetLastFeedback()
        {
            var feedback = _uow.Repository<Feedback>().Entities.ToList().LastOrDefault();
            var feedbackDTO = Mapper.Map<FeedbackDTO>(feedback);

            return feedbackDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(FeedbackDTO feedbackDTO)
        {
            var feedback = await _uow.Repository<Feedback>().UpdateAsync(Mapper.Map<Feedback>(feedbackDTO));

            if (feedback == null)
                return new ResponseDTO(false, "Данного отзыва не существует в базе данных");

            await _uow.Commit();

            return new ResponseDTO(true, "Отзыв успешно обновлен в базе данных");
        }
    }
}
