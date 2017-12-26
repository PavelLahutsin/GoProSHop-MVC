using GoProShop.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class BaseFeedbackVM : IdProvider
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Поле имя является обязательным")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Длина поля имя должна быть не меньше 3 и не больше 40 символов")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Поле email является обязательным")]
        public string Email { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Статус")]
        public FeedbackStatus Status { get; set; }

        [ScaffoldColumn(false)]
        public bool IsViewed { get; set; }

        [Display(Name = "Сообщение")]
        [Required(ErrorMessage = "Поле сообщение является обязательным")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Длина поля сообщение должна быть не меньше 3 и не больше 250 символов")]
        public string Message { get; set; }
    }
}