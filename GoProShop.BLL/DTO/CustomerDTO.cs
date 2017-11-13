using GoProShop.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.BLL.DTO
{
    public class CustomerDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(13)]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(70)]
        public string Email { get; set; }
    }
}
