namespace FreakyFashion.Payments;

/// <summary>Payments owns payment initiation and provider webhook handling.</summary>
public static class PaymentsModule
{
    public const string Name = "Payments";
}

public sealed record StartPaymentRequest(Guid OrderId, decimal Amount, string Currency, Uri ReturnUrl);
public sealed record PaymentSessionDto(string ProviderReference, Uri RedirectUrl);
public sealed record PaymentWebhook(string ProviderReference, string Status);

public interface IPaymentGateway
{
    Task<PaymentSessionDto> StartAsync(StartPaymentRequest request, CancellationToken cancellationToken = default);
}
