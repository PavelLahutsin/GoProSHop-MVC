using System.ComponentModel.DataAnnotations;

namespace GoProShop.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = "Наличные")]
        Cash = 1,
        [Display(Name = "Оплата банковской картой")]
        Card
    }
}
