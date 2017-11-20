namespace GoProShop.ViewModels
{
    public class ProductVM : IdProvider
    {
        public string Name { get; set; }

        public int ProductGroupId { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public virtual ProductGroupVM ProductGroup { get; set; }
    }
}