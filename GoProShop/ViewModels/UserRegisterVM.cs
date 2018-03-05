using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class UserRegisterVM
    {
        [Required(ErrorMessage = "Поле емейл является обязательным")]
        [EmailAddress(ErrorMessage = "Значение поля email не соответсвует формату адреса электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле логин является обязательным")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле пароль является обязательным")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле подтверждение пароля является обязательным")]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}