using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Entities;
using GoProShop.DAL.Interfaces;

namespace GoProShop.BLL.Services
{
    public class ChosenService : CrudService<Product, ChosenItemDto>, IChosenService
    {
        public ChosenService(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}
