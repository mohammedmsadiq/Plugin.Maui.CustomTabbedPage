using Microsoft.Maui.Controls;

namespace Plugin.Maui.CustomTabbedPage.Controls;

/// <summary>
/// Represents an individual tab page within <see cref="CustomTabbedPage"/>. Derive your
/// content pages from this class to enable bindable properties for icons, badges and
/// text visibility. When any of the bindable properties change, the parent tabbed
/// page will automatically refresh its native tab bar to reflect the new settings.
/// </summary>
public class CustomTabPage : ContentPage
{
    // Badge text shown in the tab bar. If this value is not null or empty and
    // <see cref="BadgeIsVisible"/> is true then a badge will be displayed.
    public static readonly BindableProperty BadgeTextProperty =
        BindableProperty.Create(
            nameof(BadgeText), typeof(string), typeof(CustomTabPage), default(string),
            propertyChanged: OnTabPropertyChanged);

    // Whether the badge is visible. If false the badge is hidden regardless of
    // <see cref="BadgeText"/>.
    public static readonly BindableProperty BadgeIsVisibleProperty =
        BindableProperty.Create(
            nameof(BadgeIsVisible), typeof(bool), typeof(CustomTabPage), false,
            propertyChanged: OnTabPropertyChanged);

    // Icon shown when this tab is selected. If null the page's existing IconImageSource
    // will be used.
    public static readonly BindableProperty SelectedIconProperty =
        BindableProperty.Create(
            nameof(SelectedIcon), typeof(ImageSource), typeof(CustomTabPage), default(ImageSource),
            propertyChanged: OnTabPropertyChanged);

    // Icon shown when this tab is not selected. If null the page's existing IconImageSource
    // will be used.
    public static readonly BindableProperty UnselectedIconProperty =
        BindableProperty.Create(
            nameof(UnselectedIcon), typeof(ImageSource), typeof(CustomTabPage), default(ImageSource),
            propertyChanged: OnTabPropertyChanged);

    // Whether to display the tab title in the tab bar. If false the title will be hidden.
    public static readonly BindableProperty ShowTextProperty =
        BindableProperty.Create(
            nameof(ShowText), typeof(bool), typeof(CustomTabPage), true,
            propertyChanged: OnTabPropertyChanged);

    // Custom font family used for the tab title. If null the default system font is used.
    public static readonly BindableProperty TabFontFamilyProperty =
        BindableProperty.Create(
            nameof(TabFontFamily), typeof(string), typeof(CustomTabPage), default(string),
            propertyChanged: OnTabPropertyChanged);

    // Custom font size used for the tab title. Defaults to 11 points.
    public static readonly BindableProperty TabFontSizeProperty =
        BindableProperty.Create(
            nameof(TabFontSize), typeof(double), typeof(CustomTabPage), 11d,
            propertyChanged: OnTabPropertyChanged);

    // Background colour of the badge.
    public static readonly BindableProperty TabBadgeColorProperty =
        BindableProperty.Create(
            nameof(TabBadgeColor), typeof(Color), typeof(CustomTabPage), Colors.Red,
            propertyChanged: OnTabPropertyChanged);

    // Foreground colour of the badge text.
    public static readonly BindableProperty TabBadgeTextColorProperty =
        BindableProperty.Create(
            nameof(TabBadgeTextColor), typeof(Color), typeof(CustomTabPage), Colors.White,
            propertyChanged: OnTabPropertyChanged);

    /// <summary>
    /// Gets or sets the badge text displayed above this tab. If null or empty no badge
    /// will be shown unless <see cref="BadgeIsVisible"/> is set to true.
    /// </summary>
    public string? BadgeText
    {
        get => (string?)GetValue(BadgeTextProperty);
        set => SetValue(BadgeTextProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the badge for this tab is visible.
    /// </summary>
    public bool BadgeIsVisible
    {
        get => (bool)GetValue(BadgeIsVisibleProperty);
        set => SetValue(BadgeIsVisibleProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon used when this tab is selected.
    /// </summary>
    public ImageSource? SelectedIcon
    {
        get => (ImageSource?)GetValue(SelectedIconProperty);
        set => SetValue(SelectedIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon used when this tab is unselected.
    /// </summary>
    public ImageSource? UnselectedIcon
    {
        get => (ImageSource?)GetValue(UnselectedIconProperty);
        set => SetValue(UnselectedIconProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tab title should be displayed in the tab bar.
    /// </summary>
    public bool ShowText
    {
        get => (bool)GetValue(ShowTextProperty);
        set => SetValue(ShowTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family used for the tab title.
    /// </summary>
    public string? TabFontFamily
    {
        get => (string?)GetValue(TabFontFamilyProperty);
        set => SetValue(TabFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size used for the tab title.
    /// </summary>
    public double TabFontSize
    {
        get => (double)GetValue(TabFontSizeProperty);
        set => SetValue(TabFontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the badge background colour.
    /// </summary>
    public Color TabBadgeColor
    {
        get => (Color)GetValue(TabBadgeColorProperty);
        set => SetValue(TabBadgeColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the badge text colour.
    /// </summary>
    public Color TabBadgeTextColor
    {
        get => (Color)GetValue(TabBadgeTextColorProperty);
        set => SetValue(TabBadgeTextColorProperty, value);
    }

    private static void OnTabPropertyChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        if (bindable is CustomTabPage page && page.Parent is CustomTabbedPage parent)
        {
            // Invalidate the tab bar so the native control updates its state.
            parent.RefreshTabs();
        }
    }
}