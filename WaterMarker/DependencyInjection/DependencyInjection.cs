using Microsoft.Extensions.DependencyInjection;
using WaterMarker.Implementation;
using WaterMarker.Implementation.Tools;
using WaterMarker.Interfaces;

namespace WaterMarker.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWatermark(this IServiceCollection services)
    {
        services.AddTransient<IWatermarkHandlerFactory, WatermarkHandlerFactory>();
        services.AddTransient<IExtentionHandler, ExtensionHandler>();
        services.AddTransient<IWatermarkGenerator, WatermarkGenerator>();

        return services;
    }
}
