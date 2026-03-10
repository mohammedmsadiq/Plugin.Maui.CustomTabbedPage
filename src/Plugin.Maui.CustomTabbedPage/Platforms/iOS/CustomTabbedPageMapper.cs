#if IOS
using CoreGraphics;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
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
    public static void Apply(IPlatformViewHandler handler, Controls.CustomTabbedPage page)
    {
        if (handler.ViewController is not UITabBarController controller)
            return;

        var tabBar = controller.TabBar;
        var items = tabBar.Items;
        var customTabs = page.Children.OfType<Controls.CustomTabPage>().ToList();

        ApplyTabBarAppearance(tabBar, page, items?.Length ?? customTabs.Count);

        if (items != null)
        {
            for (int i = 0; i < items.Length && i < customTabs.Count; i++)
            {
                var item = items[i];
                var tab = customTabs[i];

                item.Title = tab.ShowText ? tab.Title : string.Empty;
                ApplyTitleAttributes(item, page, tab);

                if (tab.BadgeIsVisible)
                {
                    item.BadgeValue = string.IsNullOrWhiteSpace(tab.BadgeText)
                        ? "●" : tab.BadgeText;
                    item.BadgeColor = tab.TabBadgeColor.ToPlatform();
                    var badgeAttrs = new UIStringAttributes { ForegroundColor = tab.TabBadgeTextColor.ToPlatform() };
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

    private static void ApplyTabBarAppearance(UITabBar tabBar, Controls.CustomTabbedPage page, int itemCount)
    {
        if (page.iOSMode != iOSTabBarMode.Branded)
        {
            tabBar.Translucent = true;
            tabBar.SelectionIndicatorImage = null;
            return;
        }

        var selectedColor = page.TabBarSelectedColor.ToPlatform();
        var unselectedColor = page.TabBarUnselectedColor.ToPlatform();
        var backgroundColor = page.TabBarBackgroundColor.ToPlatform();
        var indicatorColor = page.TabBarIndicatorColor == Colors.Transparent
            ? selectedColor
            : page.TabBarIndicatorColor.ToPlatform();
        var font = ResolveFont(page.TabFontFamily, page.TabFontSize);

        var appearance = new UITabBarAppearance();
        if (page.IsTabBarTranslucent || page.TabBarBackgroundOpacity < 1d)
        {
            appearance.ConfigureWithDefaultBackground();
        }
        else
        {
            appearance.ConfigureWithOpaqueBackground();
        }

        appearance.BackgroundColor = backgroundColor.ColorWithAlpha(
            (nfloat)Math.Clamp(page.TabBarBackgroundOpacity, 0.0, 1.0));
        appearance.ShadowColor = page.RemoveTopShadowLine
            ? UIColor.Clear
            : page.TabBarShadowColor.ToPlatform();

        ApplyLayoutAppearance(appearance.StackedLayoutAppearance, selectedColor, unselectedColor, font);
        ApplyLayoutAppearance(appearance.InlineLayoutAppearance, selectedColor, unselectedColor, font);
        ApplyLayoutAppearance(appearance.CompactInlineLayoutAppearance, selectedColor, unselectedColor, font);

        tabBar.StandardAppearance = appearance;
        if (OperatingSystem.IsIOSVersionAtLeast(15))
        {
            tabBar.ScrollEdgeAppearance = appearance;
        }

        tabBar.Translucent = page.IsTabBarTranslucent;
        tabBar.SelectionIndicatorImage = page.ShowSelectedTabUnderline
            ? CreateBottomLineIndicator(indicatorColor, tabBar.Bounds, itemCount)
            : null;
    }

    private static void ApplyLayoutAppearance(
        UITabBarItemAppearance appearance,
        UIColor selectedColor,
        UIColor unselectedColor,
        UIFont font)
    {
        appearance.Normal.IconColor = unselectedColor;
        appearance.Selected.IconColor = selectedColor;
        appearance.Normal.TitleTextAttributes = CreateTitleAttributes(unselectedColor, font);
        appearance.Selected.TitleTextAttributes = CreateTitleAttributes(selectedColor, font);
    }

    private static void ApplyTitleAttributes(
        UITabBarItem item,
        Controls.CustomTabbedPage page,
        Controls.CustomTabPage tab)
    {
        if (page.iOSMode != iOSTabBarMode.Branded)
            return;

        var font = ResolveFont(
            tab.TabFontFamily ?? page.TabFontFamily,
            tab.TabFontSize > 0 ? tab.TabFontSize : page.TabFontSize);

        item.SetTitleTextAttributes(
            CreateTitleAttributes(page.TabBarUnselectedColor.ToPlatform(), font),
            UIControlState.Normal);
        item.SetTitleTextAttributes(
            CreateTitleAttributes(page.TabBarSelectedColor.ToPlatform(), font),
            UIControlState.Selected);
    }

    private static UIStringAttributes CreateTitleAttributes(UIColor color, UIFont font) =>
        new()
        {
            ForegroundColor = color,
            Font = font
        };

    private static UIFont ResolveFont(string? fontFamily, double fontSize)
    {
        var resolvedSize = (nfloat)(fontSize > 0 ? fontSize : 11d);
        if (!string.IsNullOrWhiteSpace(fontFamily))
        {
            var namedFont = UIFont.FromName(fontFamily, resolvedSize);
            if (namedFont != null)
                return namedFont;
        }

        return UIFont.SystemFontOfSize(resolvedSize);
    }

    /// <summary>
    /// Creates a resizable UIImage that draws a thin coloured line at the bottom, used
    /// as the tab bar selection indicator image.
    /// </summary>
    private static UIImage CreateBottomLineIndicator(UIColor color, CGRect tabBarBounds, int itemCount)
    {
        const float tabItemContentHeight = 49f;
        const float lineHeight = 2.5f;
        const float additionalVerticalOffset = 10f;
        const float estimatedTitleCenterY = 35f;
        const float titleToIndicatorSpacing = 10f;
        var totalHeight = Math.Max(tabItemContentHeight, (float)tabBarBounds.Height);
        var contentHeight = Math.Min(totalHeight, tabItemContentHeight);
        var itemWidth = itemCount > 0
            ? Math.Max(1f, (float)(tabBarBounds.Width / itemCount))
            : 1f;
        var size = new CGSize(itemWidth, totalHeight);
        var desiredLineCenterY = estimatedTitleCenterY + titleToIndicatorSpacing;
        var baseLineTop = Math.Max(0f, Math.Min(contentHeight - lineHeight, desiredLineCenterY - (lineHeight / 2f)));
        var lineTop = Math.Max(0f, Math.Min(totalHeight - lineHeight, baseLineTop + additionalVerticalOffset));

        var renderer = new UIGraphicsImageRenderer(size);
        var image = renderer.CreateImage(context =>
        {
            context.CGContext.SetFillColor(color.CGColor);
            context.CGContext.FillRect(new CGRect(0f, lineTop, itemWidth, lineHeight));
        });

        return image;
    }
}
#endif
