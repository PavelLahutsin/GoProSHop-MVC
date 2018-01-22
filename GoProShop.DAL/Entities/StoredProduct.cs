namespace GoProShop.DAL.Entities
{
    public class StoredProduct : IdProvider
    {
        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Store Store { get; set; }
    }
}
