namespace GoProShop.ViewModels
{
    public class OrderedProductVM : IdProvider
    {
        public int OrderId { get; set; }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal? Discount { get; set; }

        public decimal Price { get; set; }

        public virtual ProductVM Product { get; set; }
    }
}