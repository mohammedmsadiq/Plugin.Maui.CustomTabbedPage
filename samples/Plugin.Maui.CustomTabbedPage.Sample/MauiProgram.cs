using Microsoft.Extensions.Logging;
using Plugin.Maui.CustomTabbedPage.Extensions;

namespace Plugin.Maui.CustomTabbedPage.Sample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            // Register Plugin.Maui.CustomTabbedPage so the native mappers are applied.
            .UseCustomTabbedPage();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
