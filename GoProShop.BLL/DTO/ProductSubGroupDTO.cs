using System.Collections.Generic;

namespace GoProShop.BLL.DTO
{
    public class ProductSubGroupDTO : IdProvider
    {
        public string Name { get; set; }

        public int ProductGroupId { get; set; }

        //public virtual ProductGroupDTO ProductGroup { get; set; }

        //public virtual ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();
    }
}
