using System.Collections.Generic;

namespace GoProShop.ViewModels
{
    public class ProductSubGroupVM : IdProvider
    {
        public string Name { get; set; }

        public int ProductGroupId { get; set; }

        //public virtual ProductGroupVM ProductGroup { get; set; }

        //public virtual ICollection<ProductVM> Products { get; set; } = new HashSet<ProductVM>();
    }
}