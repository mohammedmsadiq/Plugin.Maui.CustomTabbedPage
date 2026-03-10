using Microsoft.Maui.Hosting;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
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
        // tabbed view mappings are global, ensure the view is of our type
        // before invoking the mapper.
        TabbedViewHandler.Mapper.AppendToMapping("CustomTabbedPage", (handler, view) =>
        {
            if (view is not Controls.CustomTabbedPage page)
                return;
            if (handler is not IPlatformViewHandler platformHandler)
                return;
#if IOS
            Plugin.Maui.CustomTabbedPage.Platforms.iOS.CustomTabbedPageMapper.Apply(platformHandler, page);
#elif ANDROID
            Plugin.Maui.CustomTabbedPage.Platforms.Android.CustomTabbedPageMapper.Apply(platformHandler, page);
#endif
        });
        return builder;
    }
}
