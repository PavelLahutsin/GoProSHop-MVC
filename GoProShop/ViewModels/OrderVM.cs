using GoProShop.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class OrderVM : IdProvider
    {
        [Display(Name = "Дата")]
        [Required(ErrorMessage = "* Поле дата доставки является обязательным")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Способ доставки")]
        [Range(1, 2, ErrorMessage = "* Поле способ доставки является обязательным")]
        public DeliveryType DeliveryType { get; set; }

        [Display(Name = "Метод оплаты")]
        [Range(1,2, ErrorMessage = "* Поле способ оплаты является обязательным")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Cтатус")]
        public Condition Condition { get; set; }

        public int CustomerId { get; set; }

        public bool IsViewed { get; set; }

        [Display(Name = "Сумма")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        public virtual CustomerVM Customer { get; set; }

        public virtual IEnumerable<OrderedProductVM> OrdersList { get; set; } = new List<OrderedProductVM>();
    }
}