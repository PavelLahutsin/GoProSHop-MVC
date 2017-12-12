using System.Collections.Generic;

namespace GoProShop.DAL.Entities
{
    public class ProductGroup : IdProvider
    {
        public string Name { get; set; }

        public virtual ICollection<ProductSubGroup> ProductSubGroups { get; set; } = new HashSet<ProductSubGroup>();
    }
}
