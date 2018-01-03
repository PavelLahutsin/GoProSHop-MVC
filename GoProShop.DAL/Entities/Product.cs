using GoProShop.DAL.Enums;
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

        public int ProductSubGroupId { get; set; }
        
        public ProductStatus? Status { get; set; }

        [Required]
        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public string MimeType { get; set; }

        [StringLength(2500)]
        public string Description { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();

        public virtual ICollection<OrderedProduct> OrdersList { get; set; } = new HashSet<OrderedProduct>();

        public virtual ProductSubGroup ProductSubGroup { get; set; }

        public virtual ICollection<StoredProduct> StoresList { get; set; } = new HashSet<StoredProduct>();

    }
}
