using CartingService.BLL.Interfaces;
using CartingService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.WebApi.Controllers.v2;

/// <summary>
/// Controller for managing cart operations such as retrieving, adding, and removing items in a cart.
/// </summary>
[ApiVersion("2.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class CartController(ICartService cartService) : ControllerBase
{
    private readonly ICartService _cartService = cartService;

    /// <summary>
    /// Gets only the list of items in the cart.
    /// </summary>
    /// <param name="cartId">The unique key of the cart.</param>
    [HttpGet("{cartId}")]
    public IActionResult GetItems(string cartId)
    {
        // Fetches all items currently in the specified cart
        var items = _cartService.GetItems(cartId);
        return Ok(items);
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
        if (item == null) return BadRequest("Item data is required.");
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