using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProShop.DAL.Entities
{
    public class Product : IdProvider
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int ProductGroupId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        [StringLength(2500)]
        public string Description { get; set; }

        public virtual ICollection<OrderedProduct> OrdersList { get; set; } = new HashSet<OrderedProduct>();

        public virtual ProductGroup ProductGroup { get; set; }

        public virtual ICollection<StoredProduct> StoresList { get; set; } = new HashSet<StoredProduct>();

    }
}
