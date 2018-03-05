
using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = "Поле логин является обязательным")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле пароль является обязательным")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}