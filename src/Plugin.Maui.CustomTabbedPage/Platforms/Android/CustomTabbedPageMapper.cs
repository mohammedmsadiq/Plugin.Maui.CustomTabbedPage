using Android.Graphics;
using Android.Views;
using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Navigation;
using Microsoft.Maui.Controls.Handlers;
using Microsoft.Maui.Platform;

namespace Plugin.Maui.CustomTabbedPage.Platforms.Android;

/// <summary>
/// Provides a minimal customisation of the native bottom navigation view on Android
/// based on the settings from <see cref="Controls.CustomTabbedPage"/>. This implementation
/// focuses on updating text visibility and badges while keeping the underlying
/// navigation behaviour intact.
/// </summary>
public static class CustomTabbedPageMapper
{
    /// <summary>
    /// Applies appearance settings to the Android bottom navigation view.
    /// </summary>
    /// <param name="handler">The tabbed page handler.</param>
    /// <param name="page">The custom tabbed page.</param>
    public static void Apply(TabbedPageHandler handler, Controls.CustomTabbedPage page)
    {
        var nativePage = handler.PlatformView;
        if (nativePage is null)
            return;

        // Attempt to locate the BottomNavigationView within the view hierarchy.
        var bottomNav = FindBottomNavigationView(nativePage);
        if (bottomNav == null)
            return;

        // Set global colours.
        var selectedColor = page.TabBarSelectedColor.ToPlatform();
        var unselectedColor = page.TabBarUnselectedColor.ToPlatform();
        bottomNav.ItemIconTintList = CreateColorStateList(selectedColor, unselectedColor);
        bottomNav.ItemTextColor = CreateColorStateList(selectedColor, unselectedColor);

        // Iterate through menu items and apply per-tab settings.
        var menu = bottomNav.Menu;
        var tabs = page.Children.OfType<Controls.CustomTabPage>().ToList();
        for (int i = 0; i < menu.Size() && i < tabs.Count; i++)
        {
            var menuItem = menu.GetItem(i);
            var tab = tabs[i];

            // Show/hide text.
            menuItem.SetTitle(tab.ShowText ? tab.Title : string.Empty);

            // Apply badges.
            ApplyBadge(bottomNav, menuItem, tab);
        }
    }

    private static void ApplyBadge(BottomNavigationView bottomNav, IMenuItem menuItem, Controls.CustomTabPage tab)
    {
        if (!tab.BadgeIsVisible)
        {
            bottomNav.RemoveBadge(menuItem.ItemId);
            return;
        }
        var badge = bottomNav.GetOrCreateBadge(menuItem.ItemId);
        badge.Visible = true;
        badge.BackgroundColor = tab.TabBadgeColor.ToPlatform();
        badge.BadgeTextColor = tab.TabBadgeTextColor.ToPlatform();
        if (int.TryParse(tab.BadgeText, out var number))
        {
            badge.Number = number;
        }
        else
        {
            badge.ClearNumber();
        }
    }

    private static BottomNavigationView? FindBottomNavigationView(View view)
    {
        if (view is BottomNavigationView bottomNavigationView)
            return bottomNavigationView;
        if (view is ViewGroup group)
        {
            for (int i = 0; i < group.ChildCount; i++)
            {
                var result = FindBottomNavigationView(group.GetChildAt(i));
                if (result != null)
                    return result;
            }
        }
        return null;
    }

    private static Android.Content.Res.ColorStateList CreateColorStateList(Color selectedColor, Color unselectedColor)
    {
        int[][] states = new int[][]
        {
            new int[] { global::Android.Resource.Attribute.StateChecked },
            new int[] { -global::Android.Resource.Attribute.StateChecked }
        };
        int[] colors = new int[]
        {
            selectedColor.ToArgb(),
            unselectedColor.ToArgb()
        };
        return new Android.Content.Res.ColorStateList(states, colors);
    }
}