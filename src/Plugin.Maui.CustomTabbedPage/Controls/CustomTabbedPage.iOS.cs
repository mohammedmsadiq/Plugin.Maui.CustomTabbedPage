#if IOS
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
using UIKit;
#endif

namespace Plugin.Maui.CustomTabbedPage.Controls;

/// <summary>
/// iOS-specific implementation of <see cref="CustomTabbedPage.RefreshNativeTabs"/>.
/// This file is included only when compiling for iOS.
/// </summary>
public partial class CustomTabbedPage
{
#if IOS
    partial void RefreshNativeTabs()
    {
        // Ensure the handler and view controller are available.
        if (Handler is not IPlatformViewHandler handler)
            return;

        if (handler.ViewController?.View is UIView view)
        {
            view.SetNeedsLayout();
            view.LayoutIfNeeded();
        }

        // Let the mapper adjust the appearance of the tab bar.
        Plugin.Maui.CustomTabbedPage.Platforms.iOS.CustomTabbedPageMapper.Apply(handler, this);

        // Force layout to update changes.
        if (handler.ViewController?.View is UIView updatedView)
        {
            updatedView.SetNeedsLayout();
            updatedView.LayoutIfNeeded();
        }
    }
#endif
}
