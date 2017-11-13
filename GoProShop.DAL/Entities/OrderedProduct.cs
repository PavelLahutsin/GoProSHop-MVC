using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProShop.DAL.Entities
{
    public class OrderedProduct : IdProvider
    {
        public int OrderId { get; set; }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal? Discount { get; set; }

        public decimal Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public virtual Store Store { get; set; }
    }
}
