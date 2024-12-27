using Microsoft.Extensions.DependencyInjection;
using OldPhonePad.Core.Interfaces;
using OldPhonePad.Core.Services;

namespace OldPhonePad.ConsoleApp;

public static class Startup
{
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IOldPhonePadToTextService, OldPhonePadToTextService>();

        return services.BuildServiceProvider();
    }
}
