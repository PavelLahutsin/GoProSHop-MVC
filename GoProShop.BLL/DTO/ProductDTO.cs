namespace GoProShop.BLL.DTO
{
    public class ProductDTO : IdProvider
    {
        public string Name { get; set; }

        public int ProductGroupId { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

       // public virtual ProductGroupDTO ProductGroup { get; set; }
    }
}
