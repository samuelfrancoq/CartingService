using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartingService.BLL.Interfaces;
using CartingService.DAL.Interfaces;
using CartingService.Domain.Entities;

namespace CartingService.BLL.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;

    // We use Dependency Injection to get the repository
    public CartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<CartItem> GetItems(string cartId)
    {
        var cart = _repository.GetById(cartId);
        return cart.Items;
    }

    public void AddItem(string cartId, CartItem item)
    {
        var cart = _repository.GetById(cartId);
        // Business Rule: Check if the item already exists in the cart
        var existingItem = cart.Items.FirstOrDefault(i => i.Id == item.Id);

        if (existingItem != null)
        {
            // If it exists, we just update the quantity
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            // If it's new, add it to the list
            cart.Items.Add(item);
        }

        _repository.Update(cart);
    }

    public void RemoveItem(string cartId, int itemId)
    {
        var cart = _repository.GetById(cartId);
        // Remove all items that match the external system ID
        cart.Items.RemoveAll(i => i.Id == itemId);
        _repository.Update(cart);
    }

    // This method is called by the ProductUpdatedConsumer when a product is updated in the Catalog Service
    public void UpdateItemInAllCartsAsync(int productId, string newName, decimal newPrice)
    {
        // Delegate the update logic to the repository, which will handle the NoSQL database operations
        _repository.UpdateItemInAllCarts(productId, newName, newPrice);
    }
}
