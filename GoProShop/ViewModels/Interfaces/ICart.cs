using System.Collections.Generic;

namespace GoProShop.ViewModels.Interfaces
{
    public interface ICart
    {
        void Clear();

        void Remove(int productId);

        void Add(ProductVM product);
    
        decimal? TotalSum { get; }

        int Count { get; }

        IEnumerable<CartItem> CartItems { get; }
    }
}
