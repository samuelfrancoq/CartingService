using CartingService.BLL.Interfaces;
using CartingService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    // The service is injected through the constructor
    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{cartId}")]
    public IActionResult GetItems(string cartId)
    {
        // Fetches all items currently in the specified cart
        var items = _cartService.GetItems(cartId);
        return Ok(items);
    }

    [HttpPost("{cartId}/items")]
    public IActionResult AddItem(string cartId, [FromBody] CartItem item)
    {
        // Validates and adds a new item to the cart
        if (item == null) return BadRequest("Item data is required.");
        _cartService.AddItem(cartId, item);
        return Ok();
    }

    [HttpDelete("{cartId}/items/{itemId}")]
    public IActionResult RemoveItem(string cartId, int itemId)
    {
        // Removes the specified item from the cart
        _cartService.RemoveItem(cartId, itemId);
        return NoContent();
    }
}