
using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}