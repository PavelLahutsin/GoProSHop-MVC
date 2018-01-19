using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class CustomerVM : IdProvider
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле телефон является обязательным")]
        public string Phone { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Поле email является обязательным")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}