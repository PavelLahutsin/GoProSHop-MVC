using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.DAL.Entities
{
    public class Store : IdProvider
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public virtual ICollection<OrderedProduct> OrdersList { get; set; } = new HashSet<OrderedProduct>();

        public virtual ICollection<StoredProduct> StoresList { get; set; } = new HashSet<StoredProduct>();

    }
}
