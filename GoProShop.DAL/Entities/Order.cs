using GoProShop.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProShop.DAL.Entities
{
    public class Order : IdProvider
    {
        public DateTime OrderDate { get; set; }

        public Condition Condition { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        [StringLength(50)]
        public string Comment { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderedProduct> OrdersList { get; set; } = new HashSet<OrderedProduct>();
    }
}
