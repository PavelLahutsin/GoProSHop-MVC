using System;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class FeedbackVM : IdProvider
    {
        [Required(ErrorMessage = "Поле имя является обязательным")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле фамилия является обязательным")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Поле email является обязательным")]
        public string Email { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Поле сообщение является обязательным")]
        public string Message { get; set; }
    }
}