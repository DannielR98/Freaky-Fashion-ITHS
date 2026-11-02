namespace FreakyFashion.Orders;

/// <summary>Orders owns carts, order lifecycle, and order confirmation events.</summary>
public static class OrdersModule
{
    public const string Name = "Orders";
}

public sealed record CartItem(Guid ProductId, int Quantity);
public sealed record PlaceOrderRequest(Guid CustomerId, IReadOnlyList<CartItem> Items);
public sealed record OrderDto(Guid Id, Guid CustomerId, string Status, DateTimeOffset CreatedAt);

public interface IOrderService
{
    Task<OrderDto> PlaceAsync(PlaceOrderRequest request, CancellationToken cancellationToken = default);
}
