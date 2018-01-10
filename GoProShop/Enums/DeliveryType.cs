using System.ComponentModel.DataAnnotations;

namespace GoProShop.Enums
{
    public enum DeliveryType
    {
        [Display(Name = "Доставка на дом")]
        HomeDelivery = 1,
        [Display(Name = "Самовывоз")]
        SelfPickUp
    }
} 
