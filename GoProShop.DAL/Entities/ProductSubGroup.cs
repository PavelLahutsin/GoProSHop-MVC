using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.DAL.Entities
{
    public class ProductSubGroup : IdProvider
    {
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        public int ProductGroupId { get; set; }

        //public virtual ProductGroup ProductGroup { get; set; }

        //public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
