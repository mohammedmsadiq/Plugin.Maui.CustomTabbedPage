using Microsoft.Maui.Controls.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace Plugin.Maui.CustomTabbedPage.Platforms.iOS;

/// <summary>
/// A minimal mapper that synchronises the native UITabBar with settings from
/// <see cref="Controls.CustomTabbedPage"/>. The goal is to stay lightweight
/// while still providing a customisable tab bar without overriding the
/// fundamental behaviour of the native control. Developers can extend this
/// class to implement further customisations.
/// </summary>
public static class CustomTabbedPageMapper
{
    /// <summary>
    /// Applies the appearance settings for the supplied <see cref="Controls.CustomTabbedPage"/>.
    /// </summary>
    /// <param name="handler">The tabbed page handler.</param>
    /// <param name="page">The custom tabbed page.</param>
    public static void Apply(TabbedPageHandler handler, Controls.CustomTabbedPage page)
    {
        if (handler.ViewController is not UITabBarController controller)
            return;

        // Update overall bar colours if provided.
        controller.TabBar.TintColor = page.TabBarSelectedColor.ToPlatform();
        controller.TabBar.UnselectedItemTintColor = page.TabBarUnselectedColor.ToPlatform();

        // Conditionally set background colour and opacity for branded mode.
        if (page.iOSMode == iOSTabBarMode.Branded)
        {
            var backgroundColor = page.TabBarBackgroundColor.ToPlatform();
            var alpha = (nfloat)Math.Clamp(page.TabBarBackgroundOpacity, 0.0, 1.0);
            controller.TabBar.BackgroundColor = backgroundColor.ColorWithAlpha(alpha);
            controller.TabBar.Translucent = page.IsTabBarTranslucent;
        }
        else
        {
            // For NativeGlass mode restore default values.
            controller.TabBar.Translucent = true;
        }

        // Iterate through tab items and update their titles and badges.
        var items = controller.TabBar.Items;
        var customTabs = page.Children.OfType<Controls.CustomTabPage>().ToList();
        if (items != null)
        {
            for (int i = 0; i < items.Length && i < customTabs.Count; i++)
            {
                var item = items[i];
                var tab = customTabs[i];

                item.Title = tab.ShowText ? tab.Title : string.Empty;

                if (tab.BadgeIsVisible)
                {
                    item.BadgeValue = string.IsNullOrWhiteSpace(tab.BadgeText)
                        ? "●" : tab.BadgeText;
                    item.BadgeColor = tab.TabBadgeColor.ToPlatform();
                    var badgeAttrs = new UITextAttributes { TextColor = tab.TabBadgeTextColor.ToPlatform() };
                    item.SetBadgeTextAttributes(badgeAttrs, UIControlState.Normal);
                    item.SetBadgeTextAttributes(badgeAttrs, UIControlState.Selected);
                }
                else
                {
                    item.BadgeValue = null;
                }
            }
        }
    }
}