using System.ComponentModel.DataAnnotations;

namespace GoProShop.Enums
{
    public enum ProductStatus
    {
        [Display(Name = "Нет в наличии")]
        NotAvailable = 1,

        [Display(Name = "В наличии")]
        Available
    }
}
