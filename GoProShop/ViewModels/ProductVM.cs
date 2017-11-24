using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class ProductVM : IdProvider
    {
        [Display(Name = "Название")]
        public string Name { get; set; }

        public int ProductGroupId { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public virtual ProductGroupVM ProductGroup { get; set; }
    }
}