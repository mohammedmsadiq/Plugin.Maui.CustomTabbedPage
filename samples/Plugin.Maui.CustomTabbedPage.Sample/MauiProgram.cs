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

        return builder.Build();
    }
}
