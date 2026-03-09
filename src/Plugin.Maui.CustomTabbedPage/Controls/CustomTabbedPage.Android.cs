using Microsoft.Maui.Controls.Handlers;
using Android.Views;

namespace Plugin.Maui.CustomTabbedPage.Controls;

/// <summary>
/// Android-specific implementation of <see cref="CustomTabbedPage.RefreshNativeTabs"/>.
/// This file is included only when compiling for Android.
/// </summary>
public partial class CustomTabbedPage
{
#if ANDROID
    partial void RefreshNativeTabs()
    {
        if (Handler is not TabbedPageHandler handler)
            return;

        // Apply appearance customisations via the mapper.
        Plugin.Maui.CustomTabbedPage.Platforms.Android.CustomTabbedPageMapper.Apply(handler, this);

        // Request a layout update on the native view.
        if (handler.PlatformView is View nativeView)
        {
            nativeView.Post(() =>
            {
                nativeView.RequestLayout();
                nativeView.Invalidate();
            });
        }
    }
#endif
}