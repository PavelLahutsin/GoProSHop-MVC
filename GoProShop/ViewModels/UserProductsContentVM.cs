using PagedList;
using System.Linq;

namespace GoProShop.ViewModels
{
    public class UserProductsContentVM
    {
        public int? SubGroupId => Products?.FirstOrDefault().ProductSubGroupId;

        public IPagedList<ProductVM> Products { get; set; }
    }
}