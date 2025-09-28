using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs;
using Shop.Application.Interfaces;
using System.Security.Claims;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService) => _orderService = orderService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        if (userId is null)
        {
            return Unauthorized(new
            {
                errorCode = "UNAUTHORIZED",
                message = "You must be logged in to create an order."
            });
        }

        var order = await _orderService.CreateAsync(dto, userId, cancellationToken);

        if (order is null)
        {
            return BadRequest(new
            {
                errorCode = "INVALID_ORDER",
                message = "One or more products are invalid or out of stock."
            });
        }

        return CreatedAtAction(nameof(GetAll), new { id = order.Id }, order);
    }

    private int? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return null;
        return int.TryParse(userIdClaim.Value, out var userId) ? userId : null;
    }
}
