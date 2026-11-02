namespace FreakyFashion.Catalog;

/// <summary>Catalog module entry point. Catalog owns product data and read models.</summary>
public static class CatalogModule
{
    public const string Name = "Catalog";
}

public sealed record ProductDto(Guid Id, string Name, string Description, decimal Price, string Currency, string ImageUrl);

public interface IProductCatalog
{
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductDto>> SearchAsync(string? query, CancellationToken cancellationToken = default);
}
