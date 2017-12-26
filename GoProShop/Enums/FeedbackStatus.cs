using System.ComponentModel.DataAnnotations;

namespace GoProShop.Enums
{
    public enum FeedbackStatus
    {
        [Display(Name="В ожидании")]
        Pending = 0,
        [Display(Name = "Отклонен")]
        NotApproved,
        [Display(Name = "Опубликован")]
        Approved
    }
}
