using GoProShop.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class FeedbackVM : BaseFeedbackVM
    {
        [Required(ErrorMessage = "Поле фамилия является обязательным")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина поля фамилия должна быть не меньше 3 и не больше 20 символов")]
        [ScaffoldColumn(false)]
        public string SurName { get; set; }
    }
}