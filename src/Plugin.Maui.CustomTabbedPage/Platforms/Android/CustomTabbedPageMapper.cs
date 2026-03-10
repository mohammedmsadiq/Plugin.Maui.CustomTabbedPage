#if ANDROID
using Android.Graphics.Drawables;
using Android.Views;
using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Navigation;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
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
    public static void Apply(IPlatformViewHandler handler, Controls.CustomTabbedPage page)
    {
        var nativePage = handler.PlatformView;
        if (nativePage is null)
            return;

        // Attempt to locate the BottomNavigationView within the handler's view hierarchy.
        var bottomNav = FindBottomNavigationView(nativePage);

        // Fallback: search from the Activity's content root in case MAUI places the
        // BottomNavigationView outside the handler's own subtree.
        if (bottomNav == null)
        {
            var activity = handler.MauiContext?.Context as global::Android.App.Activity;
            var contentRoot = activity?.FindViewById<global::Android.Views.View>(global::Android.Resource.Id.Content);
            if (contentRoot != null)
                bottomNav = FindBottomNavigationView(contentRoot);
        }

        if (bottomNav == null)
            return;

        // Set background colour.
        bottomNav.SetBackgroundColor(page.TabBarBackgroundColor.ToPlatform());

        var density = bottomNav.Resources?.DisplayMetrics?.Density ?? 3f;
        var targetHeight = (int)(70f * density);
        if (bottomNav.LayoutParameters is { } lp && lp.Height != targetHeight)
        {
            lp.Height = targetHeight;
            bottomNav.LayoutParameters = lp;
        }

        // Top padding: pushes the icon (and its badge) away from the top edge of the bar.
        // Bottom padding: lifts the text label so it is not covered by the overlay line
        //                 which is drawn in the last 2.5 dp of the bar.
        var topPaddingPx    = (int)(5f * density);
        var bottomPaddingPx = (int)(12f * density);
        bottomNav.SetPadding(bottomNav.PaddingLeft, topPaddingPx, bottomNav.PaddingRight, bottomPaddingPx);

        // Always show labels for every item regardless of count.  Without this,
        // Material Design auto-hides unselected labels when there are ≥4 items,
        // which makes the bar look cramped/narrow.
        bottomNav.LabelVisibilityMode = NavigationBarView.LabelVisibilityLabeled;

        // Hide the Material 3 active-indicator pill so only the icon/text colour
        // signals the selected state (the pill colour is made transparent; its height
        // is left at the Material default so the icon remains correctly positioned).
        bottomNav.ItemActiveIndicatorColor =
            global::Android.Content.Res.ColorStateList.ValueOf(global::Android.Graphics.Color.Transparent);

        // Set global colours.
        var selectedColor = page.TabBarSelectedColor.ToPlatform();
        var unselectedColor = page.TabBarUnselectedColor.ToPlatform();
        bottomNav.ItemIconTintList = CreateColorStateList(selectedColor, unselectedColor);
        bottomNav.ItemTextColor = CreateColorStateList(selectedColor, unselectedColor);

        // Apply per-item selection indicator line via ViewOverlay so it always
        // renders on top regardless of Material 3 item background handling.
        // Use MAUI's CurrentPage as the source of truth for the selected index —
        // the native MenuItem.IsChecked state may lag behind when Post() runs.
        var overlay = bottomNav.Overlay;
        overlay?.Clear();
        if (page.ShowSelectedTabUnderline && overlay != null)
        {
            var indicatorColor = page.TabBarIndicatorColor == Colors.Transparent
                ? selectedColor
                : page.TabBarIndicatorColor.ToPlatform();
            var lineHeightPx = (int)Math.Round(2.5f * density);
            int selectedIndex = page.Children.IndexOf(page.CurrentPage);
            if (selectedIndex < 0) selectedIndex = 0;
            // Pass targetHeight explicitly — bottomNav.Height may be stale or transitional
            // during a MAUI page-switch layout pass, causing the overlay to render off-screen.
            // Subtract 5 dp so the line floats slightly above the absolute bottom edge.
            var bottomOffsetPx = (int)(7f * density);
            DrawSelectionIndicator(bottomNav, overlay, indicatorColor, lineHeightPx, selectedIndex, targetHeight - bottomOffsetPx);
        }

        // Iterate through menu items and apply per-tab settings.
        var menu = bottomNav.Menu;
        var tabs = page.Children.OfType<Controls.CustomTabPage>().ToList();
        for (int i = 0; i < menu.Size() && i < tabs.Count; i++)
        {
            var menuItem = menu.GetItem(i);
            if (menuItem == null)
                continue;

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

    private static BottomNavigationView? FindBottomNavigationView(global::Android.Views.View view)
    {
        if (view is BottomNavigationView bottomNavigationView)
            return bottomNavigationView;
        if (view is ViewGroup group)
        {
            for (int i = 0; i < group.ChildCount; i++)
            {
                var childView = group.GetChildAt(i);
                if (childView == null)
                    continue;

                var result = FindBottomNavigationView(childView);
                if (result != null)
                    return result;
            }
        }
        return null;
    }

    // Parameters are Android.Graphics.Color (returned by ToPlatform()) — use global:: to
    // avoid ambiguity with Microsoft.Maui.Graphics.Color which is also in scope.
    // global:: is also required on Android.Content.Res because the current namespace
    // (Plugin.Maui.CustomTabbedPage.Platforms.Android) makes the compiler look for
    // Android as a child namespace first, causing CS0234 without the global:: prefix.
    private static global::Android.Content.Res.ColorStateList CreateColorStateList(
        global::Android.Graphics.Color selectedColor,
        global::Android.Graphics.Color unselectedColor)
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
        return new global::Android.Content.Res.ColorStateList(states, colors);
    }

    /// <summary>
    /// Draws a thin coloured line at the top of the selected item using the view's
    /// <see cref="ViewOverlay"/> so it is always rendered on top regardless of how
    /// Material 3 handles item backgrounds internally.
    /// </summary>
    /// <param name="barHeightPx">
    /// The intended bar height in pixels (our <c>targetHeight</c>).  We use this
    /// instead of <c>bottomNav.Height</c> because the measured height can be stale
    /// or reflect a transitional layout state during MAUI page switches, which would
    /// place the overlay drawable outside the bar's visible bounds.
    /// </param>
    private static void DrawSelectionIndicator(
        BottomNavigationView bottomNav,
        ViewOverlay overlay,
        global::Android.Graphics.Color indicatorColor,
        int lineHeightPx,
        int selectedIndex,
        int barHeightPx)
    {
        // Width is only known after layout; skip silently — Loaded will re-invoke Apply.
        if (bottomNav.Width == 0)
            return;

        int itemCount = bottomNav.Menu.Size();
        if (itemCount == 0 || selectedIndex < 0 || selectedIndex >= itemCount)
            return;

        int itemWidth = bottomNav.Width / itemCount;
        int left = selectedIndex * itemWidth;

        var line = new GradientDrawable();
        line.SetColor(indicatorColor);
        // Use barHeightPx (the value we set on LayoutParams) for the Y coordinate,
        // not bottomNav.Height, so the line is always at the bottom of the styled bar.
        line.SetBounds(left, barHeightPx - lineHeightPx, left + itemWidth, barHeightPx);
        overlay.Add(line);
    }
}
#endif
