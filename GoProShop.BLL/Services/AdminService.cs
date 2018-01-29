using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Interfaces;
using System;
using System.Linq;

namespace GoProShop.BLL.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(IUnitOfWork uow)
            : base(uow)
        {
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
