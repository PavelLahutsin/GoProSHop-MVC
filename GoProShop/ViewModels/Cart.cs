﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GoProShop.ViewModels
{
    public class Cart
    {
        private readonly IList<CartItem> _cartItems = new List<CartItem>();

        public void Add(ProductVM product)
        {
            var cartItem = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);

            if (cartItem == null)
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

        public void Reduce(int productId)
        {
            var cartItem = _cartItems.FirstOrDefault(x => x.Product.Id == productId);

            if (cartItem?.Quantity == 1)
            {
                _cartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity--;
            }
        }

        public void Remove(int productId)
        {
            var cartItem = _cartItems.FirstOrDefault(x => x.Product.Id == productId);
            _cartItems.Remove(cartItem);
        }

        [Display(Name = "Итого:")]
        public decimal? TotalSum => _cartItems?.Sum(x => x.Product.Price * x.Quantity);

        public int Count => _cartItems.Sum(x => x.Quantity);

        public IEnumerable<CartItem> CartItems => _cartItems;
    }
}