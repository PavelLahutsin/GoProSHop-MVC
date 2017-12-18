using System;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class FeedbackVM : IdProvider
    {
        [Required(ErrorMessage = "Поле имя является обязательным")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина поля имя должна быть не меньше 3 и не больше 20 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле фамилия является обязательным")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина поля фамилия должна быть не меньше 3 и не больше 20 символов")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Поле email является обязательным")]
        public string Email { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Поле сообщение является обязательным")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина поля сообщение должна быть не меньше 3 и не больше 250 символов")]
        public string Message { get; set; }
    }
}