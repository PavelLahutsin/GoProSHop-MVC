using GoProShop.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GoProShop.ViewModels
{
    public class ProductVM : IdProvider
    {
        [Required(ErrorMessage = "Поле название является обязательным")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле группа является обязательным")]
        [Display(Name = "Группа")]
        public int ProductSubGroupId { get; set; }

        [Required(ErrorMessage = "Поле цена является обязательным")]
        [Range(typeof(decimal),"0.00","99999.99", ErrorMessage = "Цена должна быть от 0.00 до 99999.99 бел.рублей")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Фото")]
        public byte[] Image { get; set; }

        [ScaffoldColumn(false)]
        public string MimeType { get; set; }

        [Display(Name= "Описание")]
        public string Description { get; set; }

        [Display(Name = "Статус")]
        public ProductStatus? Status { get; set; }
    }
}