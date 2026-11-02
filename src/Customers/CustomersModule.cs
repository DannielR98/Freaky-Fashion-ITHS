namespace FreakyFashion.Customers;

/// <summary>Customers owns customer profiles and authentication integration.</summary>
public static class CustomersModule
{
    public const string Name = "Customers";
}

public sealed record CustomerDto(Guid Id, string Email, string DisplayName);

public interface ICustomerDirectory
{
    Task<CustomerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
