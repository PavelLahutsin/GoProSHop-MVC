using System.Collections.Generic;


namespace GoProShop.ViewModels
{
    public class ProductGroupVM : IdProvider
    {
        public string Name { get; set; }

        public virtual ICollection<ProductSubGroupVM> ProductSubGroups { get; set; } = new HashSet<ProductSubGroupVM>();
    }
}