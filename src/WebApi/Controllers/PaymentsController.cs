using FreakyFashion.Payments;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashion.WebApi.Controllers;

[ApiController]
[Route("api/payments")]
public sealed class PaymentsController(IPaymentGateway gateway) : ControllerBase
{
    [HttpPost("sessions")]
    public async Task<ActionResult<PaymentSessionDto>> Start([FromBody] StartPaymentRequest request, CancellationToken cancellationToken) =>
        Ok(await gateway.StartAsync(request, cancellationToken));

    [HttpPost("webhooks/{provider}")]
    public IActionResult ReceiveWebhook(string provider, [FromBody] PaymentWebhook webhook) =>
        Accepted(new { provider, webhook.ProviderReference, webhook.Status });
}
