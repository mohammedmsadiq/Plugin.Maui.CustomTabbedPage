#if ANDROID
using Android.Views;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
#endif

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
        if (Handler is not IPlatformViewHandler handler)
            return;

        if (handler.PlatformView is not global::Android.Views.View nativeView)
            return;

        // Post so the BottomNavigationView is guaranteed to be in the view hierarchy
        // before we attempt to style it.
        nativeView.Post(() =>
        {
            Plugin.Maui.CustomTabbedPage.Platforms.Android.CustomTabbedPageMapper.Apply(handler, this);
            nativeView.RequestLayout();
            nativeView.Invalidate();
        });
    }
#endif
}
