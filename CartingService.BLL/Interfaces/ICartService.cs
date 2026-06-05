using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartingService.Domain.Entities;

namespace CartingService.BLL.Interfaces
{
    public interface ICartService
    {
        // Returns the list of items for a specific cart
        IEnumerable<CartItem> GetItems(string cartId);
        // Adds an item or updates quantity if it already exists
        void AddItem(string cartId, CartItem item);
        // Removes a specific item from the cart
        void RemoveItem(string cartId, int itemId);
        void UpdateItemInAllCartsAsync(int productId, string newName, decimal newPrice);

    }
}
