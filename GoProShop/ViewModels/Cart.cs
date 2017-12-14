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
        private readonly IList<CartItem> _cartItems = new List<CartItem>();

        public void Add(ProductVM product)
        {
            var cartItem = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);

            if(cartItem == null)
            {
                _cartItems.Add(new CartItem
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
            _cartItems.Clear();
        }

        public void Remove(ProductVM product)
        {
            var cartItem = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);

            if(cartItem?.Quantity == 1)
            {
                _cartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity--;
            }    
        }

        [Display(Name = "Итого:")]
        public decimal? TotalSum => _cartItems?.Sum(x => x.Product.Price * x.Quantity);

        public int Count => _cartItems.Count;

        public IEnumerable<CartItem> CartItems => _cartItems;
    }
}