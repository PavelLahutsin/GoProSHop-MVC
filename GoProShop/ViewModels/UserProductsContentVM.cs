using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoProShop.ViewModels
{
    public class UserProductsContentVM
    {
        public int? SubGroupId => Products?.FirstOrDefault().ProductSubGroupId;

        public IPagedList<ProductVM> Products { get; set; }
    }
}