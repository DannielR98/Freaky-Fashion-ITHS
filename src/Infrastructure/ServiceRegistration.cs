using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreakyFashion.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddFreakyFashionInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AzureServiceOptions>()
            .Bind(configuration.GetSection(AzureServiceOptions.SectionName));

        // Azure SQL, Managed Redis, and Blob Storage adapters are intentionally added
        // behind these seams when their SDK packages and deployment resources are selected.
        return services;
    }
}
