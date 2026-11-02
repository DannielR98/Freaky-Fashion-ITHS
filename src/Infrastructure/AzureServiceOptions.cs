namespace FreakyFashion.Infrastructure;

/// <summary>All values are supplied by environment variables/Azure App Settings (optionally Key Vault references).</summary>
public sealed class AzureServiceOptions
{
    public const string SectionName = "AzureServices";
    public string? SqlConnectionString { get; init; }
    public string? RedisConnectionString { get; init; }
    public string? BlobServiceUri { get; init; }
}

public interface IProductImageStore
{
    Task<Uri> UploadAsync(string fileName, Stream content, string contentType, CancellationToken cancellationToken = default);
}

/// <summary>Infrastructure seam for a Redis-backed catalog cache.</summary>
public interface ICatalogCache
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, TimeSpan timeToLive, CancellationToken cancellationToken = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}
