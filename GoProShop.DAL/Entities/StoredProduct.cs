using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoProShop.DAL.Entities
{
    public class StoredProduct : IdProvider
    {
        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public int? Endings { get; set; }

        public virtual Product Product { get; set; }

        public virtual Store Store { get; set; }
    }
}
