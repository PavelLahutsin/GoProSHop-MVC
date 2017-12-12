using GoProShop.BLL.Enums;

namespace GoProShop.BLL.DTO
{
    public class ProductDTO : IdProvider
    {
        public string Name { get; set; }

        public int ProductSubGroupId { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public string MimeType { get; set; }

        public string Description { get; set; }

        public ProductStatus? Status { get; set; }

        //public virtual ProductSubGroupDTO ProductSubGroup { get; set; }
    }
}
