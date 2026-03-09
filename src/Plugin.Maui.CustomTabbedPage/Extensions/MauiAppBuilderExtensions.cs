using Microsoft.Maui.Controls.Handlers;
using Microsoft.Maui.Hosting;
using Plugin.Maui.CustomTabbedPage.Controls;

namespace Plugin.Maui.CustomTabbedPage.Extensions;

/// <summary>
/// Provides an extension method for <see cref="MauiAppBuilder"/> to enable
/// the custom tabbed page mappings on supported platforms. Add a call
/// to <c>UseCustomTabbedPage()</c> in your <c>CreateMauiApp</c> method to
/// activate the control.
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Configures the application builder to support the custom tabbed page.
    /// </summary>
    /// <param name="builder">The <see cref="MauiAppBuilder"/>.</param>
    /// <returns>The builder for chaining.</returns>
    public static MauiAppBuilder UseCustomTabbedPage(this MauiAppBuilder builder)
    {
        // Append a mapper that calls into our platform specific mappers. Because
        // TabbedPageHandler mappings are global, ensure the view is of our type
        // before invoking the mapper.
        TabbedPageHandler.Mapper.AppendToMapping("CustomTabbedPage", (handler, view) =>
        {
            if (view is not CustomTabbedPage page)
                return;
#if IOS
            Plugin.Maui.CustomTabbedPage.Platforms.iOS.CustomTabbedPageMapper.Apply(handler, page);
#elif ANDROID
            Plugin.Maui.CustomTabbedPage.Platforms.Android.CustomTabbedPageMapper.Apply(handler, page);
#endif
        });
        return builder;
    }
}