using CartingService.BLL.Interfaces;
using CartingService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.WebApi.Controllers.v1;

/// <summary>
/// Controller for managing cart operations such as retrieving, adding, and removing items in a cart.
/// </summary>
/// 
[Authorize(Roles = "Manager,Store customer")]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class CartController(ICartService cartService) : ControllerBase
{
    private readonly ICartService _cartService = cartService;

    /// <summary>
    /// Gets the cart information including the key and the list of items.
    /// </summary>
    /// <param name="cartId">The unique key of the cart.</param>
    /// <returns>The cart information with items.</returns>
    [HttpGet("{cartId}")]
    public IActionResult GetItems(string cartId)
    {
        // Fetches all items currently in the specified cart
        var items = _cartService.GetItems(cartId);
        return Ok(new { CartKey = cartId, Items = items });
    }

    /// <summary>
    /// Adds a new item to the cart.
    /// </summary>
    /// <param name="cartId"></param>
    /// <param name="item"></param>
    [HttpPost("{cartId}/items")]
    public IActionResult AddItem(string cartId, [FromBody] CartItem item)
    {
        // Validates and adds a new item to the cart
        if (item == null)
        {
            return BadRequest("Item data is required.");
        }
        _cartService.AddItem(cartId, item);
        return Ok();
    }

    /// <summary>
    /// Removes the specified item from the cart.
    /// </summary>
    /// <param name="cartId"></param>
    /// <param name="itemId"></param>
    [HttpDelete("{cartId}/items/{itemId}")]
    public IActionResult RemoveItem(string cartId, int itemId)
    {
        // Removes the specified item from the cart
        _cartService.RemoveItem(cartId, itemId);
        return NoContent();
    }
}
