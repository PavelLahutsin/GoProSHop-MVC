using GoProShop.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoProShop.ViewModels
{
    public class Cart : ICart
    {
        private readonly IList<CartItem> cartItems = new List<CartItem>();

        public void Add(ProductVM product)
        {
            var cartItem = cartItems.FirstOrDefault(x => x.Product.Id == product.Id);

            if(cartItem == null)
            {
                cartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        public void Clear()
        {
            cartItems.Clear();
        }

        public void Remove(ProductVM product)
        {
            var cartItem = cartItems.FirstOrDefault(x => x.Product.Id == product.Id);

            if(cartItem.Quantity == 1)
            {
                cartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity--;
            }

          
        }

        [Display(Name = "Итого:")]
        public decimal? TotalSum => cartItems?.Sum(x => x.Product.Price * x.Quantity);

        public int Count => cartItems.Select(x => x.Quantity).Count();

        public IEnumerable<CartItem> CartItems => cartItems;
    }
}