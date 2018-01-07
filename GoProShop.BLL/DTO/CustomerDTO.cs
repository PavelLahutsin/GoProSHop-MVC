namespace GoProShop.BLL.DTO
{
    public class CustomerDTO : IdProvider
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
