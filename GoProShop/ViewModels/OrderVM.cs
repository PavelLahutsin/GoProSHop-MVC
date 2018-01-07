using GoProShop.Enums;
using System;
using System.Collections.Generic;

namespace GoProShop.ViewModels
{
    public class OrderVM : IdProvider
    {
        public DateTime OrderDate { get; set; }

        public Condition Condition { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        public string Comment { get; set; }

        public virtual CustomerVM Customer { get; set; }

        public virtual IEnumerable<OrderedProductVM> OrdersList { get; set; } = new List<OrderedProductVM>();
    }
}