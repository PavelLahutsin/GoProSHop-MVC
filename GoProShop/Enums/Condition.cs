using System.ComponentModel.DataAnnotations;

namespace GoProShop.Enums
{
    public enum Condition
    {
        [Display(Name = "В ожидании")]
        Awaiting = 1,
        [Display(Name = "Выполнен")]
        Done = 2
    }
}