using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using System;
using System.Linq;

namespace GoProShop.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _uow;

        public AdminService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public AdminPageDTO Build()
        {
            var newFeedbacksCount = _uow.Feedbacks.Entities.Where(x => !x.IsViewed).Count();
            var newOrdersCount = _uow.Orders.Entities.Where(x => !x.IsViewed).Count();

            return new AdminPageDTO
            {
                NewFeedbacksCount = newFeedbacksCount,
                NewOrdersCount = newOrdersCount
            };
        }
    }
}
