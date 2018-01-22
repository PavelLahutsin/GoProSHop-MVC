using GoProShop.BLL.Enums;
using System;
using System.Collections.Generic;

namespace GoProShop.BLL.DTO
{
    public class OrderDTO : IdProvider
    {
        public DateTime OrderDate { get; set; }

        public Condition Condition { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int CustomerId { get; set; }

        public bool IsViewed { get; set; }

        public decimal TotalPrice { get; set; }

        public string Comment { get; set; }

        public virtual CustomerDTO Customer { get; set; }

        public virtual IEnumerable<OrderedProductDTO> OrdersList { get; set; } = new List<OrderedProductDTO>();
    }
}
