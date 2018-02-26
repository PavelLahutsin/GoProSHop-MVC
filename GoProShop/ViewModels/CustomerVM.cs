using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class CustomerVM : IdProvider
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "* Поле телефон является обязательным")]
        public string Phone { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "* Поле email является обязательным")]
        [EmailAddress(ErrorMessage = "* Значение поля email не соответсвует формату адреса электронной почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}