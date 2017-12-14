using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class CartItem
    {
        public ProductVM Product { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
    }
}