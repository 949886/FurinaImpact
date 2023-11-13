using Microsoft.Extensions.DependencyInjection;

namespace FurinaImpact.Common.Data.Provider;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseLocalAssets(this IServiceCollection services)
    {
        return services.AddSingleton<IAssetProvider, LocalAssetProvider>();
    }
}
