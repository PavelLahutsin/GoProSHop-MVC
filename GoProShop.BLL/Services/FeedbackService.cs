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

        public IEnumerable<FeedbackDTO> GetHomeFeedbacks()
        {
            var feedbacks = _uow.Feedbacks.Entities.Where(x => x.IsApproved == true).Take(3).ToList();
            var feedbacksDTO = Mapper.Map<List<Feedback>, IEnumerable<FeedbackDTO>>(feedbacks);

            return feedbacksDTO;
        }
    }
}
