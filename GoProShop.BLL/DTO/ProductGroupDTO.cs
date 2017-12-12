using System.Collections.Generic;

namespace GoProShop.BLL.DTO
{
    public class ProductGroupDTO : IdProvider
    {
        public string Name { get; set; }

        public virtual ICollection<ProductSubGroupDTO> ProductSubGroups { get; set; } = new HashSet<ProductSubGroupDTO>();
    }
}
