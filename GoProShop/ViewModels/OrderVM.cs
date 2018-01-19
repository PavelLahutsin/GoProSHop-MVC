using GoProShop.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoProShop.ViewModels
{
    public class OrderVM : IdProvider
    {
        [Required(ErrorMessage = "Поле дата доставки является обязательным")]
        public DateTime OrderDate { get; set; }

        [Range(1, 2, ErrorMessage = "Поле способ доставки является обязательным")]
        public DeliveryType DeliveryType { get; set; }

        [Range(1,2, ErrorMessage = "Поле способ оплаты является обязательным")]
        public PaymentMethod PaymentMethod { get; set; }

        public Condition Condition { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalPrice { get; set; }
        
        public string Comment { get; set; }

        public virtual CustomerVM Customer { get; set; }

        public virtual IEnumerable<OrderedProductVM> OrdersList { get; set; } = new List<OrderedProductVM>();
    }
}