using FreakyFashion.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashion.WebApi.Controllers;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController(IOrderService orders) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Place([FromBody] PlaceOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await orders.PlaceAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetStatus), new { id = order.Id }, order);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetStatus(Guid id) => Problem("Order lookup is not configured yet.", statusCode: StatusCodes.Status501NotImplemented);
}
