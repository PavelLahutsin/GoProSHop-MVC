using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class UserEditVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User name:")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old password:")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords are not matched")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}