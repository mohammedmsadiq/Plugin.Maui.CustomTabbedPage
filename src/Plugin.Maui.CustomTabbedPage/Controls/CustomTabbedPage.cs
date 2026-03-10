using Microsoft.Maui.Controls;
using Plugin.Maui.CustomTabbedPage;

namespace Plugin.Maui.CustomTabbedPage.Controls;

/// <summary>
/// A TabbedPage derivative that exposes additional bindable properties for
/// fine grained control of the appearance of each tab. It also coordinates
/// refreshing of the native tab bar on both iOS and Android whenever a
/// relevant property changes. To enable your application to use this control
/// call <see cref="Extensions.MauiAppBuilderExtensions.UseCustomTabbedPage"/> in your
/// <c>MauiProgram.cs</c> startup.
/// </summary>
public partial class CustomTabbedPage : TabbedPage
{
    #region iOS bar appearance

    public static readonly BindableProperty iOSModeProperty =
        BindableProperty.Create(
            nameof(iOSMode), typeof(iOSTabBarMode), typeof(CustomTabbedPage), iOSTabBarMode.Branded,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty TabBarBackgroundColorProperty =
        BindableProperty.Create(
            nameof(TabBarBackgroundColor), typeof(Color), typeof(CustomTabbedPage), Colors.Transparent,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty TabBarSelectedColorProperty =
        BindableProperty.Create(
            nameof(TabBarSelectedColor), typeof(Color), typeof(CustomTabbedPage), Colors.Black,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty TabBarUnselectedColorProperty =
        BindableProperty.Create(
            nameof(TabBarUnselectedColor), typeof(Color), typeof(CustomTabbedPage), Colors.Gray,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty TabBarShadowColorProperty =
        BindableProperty.Create(
            nameof(TabBarShadowColor), typeof(Color), typeof(CustomTabbedPage), Colors.Transparent,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty TabBarBackgroundOpacityProperty =
        BindableProperty.Create(
            nameof(TabBarBackgroundOpacity), typeof(double), typeof(CustomTabbedPage), 0.88d,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty IsTabBarTranslucentProperty =
        BindableProperty.Create(
            nameof(IsTabBarTranslucent), typeof(bool), typeof(CustomTabbedPage), true,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty RemoveTopShadowLineProperty =
        BindableProperty.Create(
            nameof(RemoveTopShadowLine), typeof(bool), typeof(CustomTabbedPage), true,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty TabFontFamilyProperty =
        BindableProperty.Create(
            nameof(TabFontFamily), typeof(string), typeof(CustomTabbedPage), default(string),
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty TabFontSizeProperty =
        BindableProperty.Create(
            nameof(TabFontSize), typeof(double), typeof(CustomTabbedPage), 11d,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty ShowSelectedTabUnderlineProperty =
        BindableProperty.Create(
            nameof(ShowSelectedTabUnderline), typeof(bool), typeof(CustomTabbedPage), false,
            propertyChanged: OnTabbedPagePropertyChanged);

    public static readonly BindableProperty TabBarIndicatorColorProperty =
        BindableProperty.Create(
            nameof(TabBarIndicatorColor), typeof(Color), typeof(CustomTabbedPage), Colors.Transparent,
            propertyChanged: OnTabbedPagePropertyChanged);

    /// <summary>
    /// Gets or sets the visual mode for the iOS tab bar. The default is <see cref="iOSTabBarMode.Branded"/>.
    /// </summary>
    public iOSTabBarMode iOSMode
    {
        get => (iOSTabBarMode)GetValue(iOSModeProperty);
        set => SetValue(iOSModeProperty, value);
    }

    /// <summary>
    /// Gets or sets the background colour for the iOS tab bar in Branded mode.
    /// </summary>
    public Color TabBarBackgroundColor
    {
        get => (Color)GetValue(TabBarBackgroundColorProperty);
        set => SetValue(TabBarBackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the colour of selected tab icons and titles on iOS.
    /// </summary>
    public Color TabBarSelectedColor
    {
        get => (Color)GetValue(TabBarSelectedColorProperty);
        set => SetValue(TabBarSelectedColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the colour of unselected tab icons and titles on iOS.
    /// </summary>
    public Color TabBarUnselectedColor
    {
        get => (Color)GetValue(TabBarUnselectedColorProperty);
        set => SetValue(TabBarUnselectedColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the colour of the shadow line above the iOS tab bar.
    /// </summary>
    public Color TabBarShadowColor
    {
        get => (Color)GetValue(TabBarShadowColorProperty);
        set => SetValue(TabBarShadowColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the opacity applied to the tab bar background on iOS.
    /// </summary>
    public double TabBarBackgroundOpacity
    {
        get => (double)GetValue(TabBarBackgroundOpacityProperty);
        set => SetValue(TabBarBackgroundOpacityProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the iOS tab bar should be translucent.
    /// </summary>
    public bool IsTabBarTranslucent
    {
        get => (bool)GetValue(IsTabBarTranslucentProperty);
        set => SetValue(IsTabBarTranslucentProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the separator line above the iOS tab bar
    /// should be removed.
    /// </summary>
    public bool RemoveTopShadowLine
    {
        get => (bool)GetValue(RemoveTopShadowLineProperty);
        set => SetValue(RemoveTopShadowLineProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family used for tab titles on iOS when not overridden on a per tab basis.
    /// </summary>
    public string? TabFontFamily
    {
        get => (string?)GetValue(TabFontFamilyProperty);
        set => SetValue(TabFontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size used for tab titles on iOS when not overridden on a per tab basis.
    /// </summary>
    public double TabFontSize
    {
        get => (double)GetValue(TabFontSizeProperty);
        set => SetValue(TabFontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether a thin underline should be drawn beneath
    /// the selected tab item on iOS.
    /// </summary>
    public bool ShowSelectedTabUnderline
    {
        get => (bool)GetValue(ShowSelectedTabUnderlineProperty);
        set => SetValue(ShowSelectedTabUnderlineProperty, value);
    }

    /// <summary>
    /// Gets or sets the colour of the selected tab underline indicator.
    /// Defaults to <see cref="TabBarSelectedColor"/> when Transparent.
    /// </summary>
    public Color TabBarIndicatorColor
    {
        get => (Color)GetValue(TabBarIndicatorColorProperty);
        set => SetValue(TabBarIndicatorColorProperty, value);
    }

    /// <summary>
    /// Constructs a new instance of <see cref="CustomTabbedPage"/> and wires up
    /// refresh logic for when the current tab changes or when the page is loaded.
    /// On Android the tab bar is forced to the bottom so that a
    /// <c>BottomNavigationView</c> is created and the appearance customisations
    /// applied by <see cref="Platforms.Android.CustomTabbedPageMapper"/> take effect.
    /// </summary>
    public CustomTabbedPage()
    {
#if ANDROID
        Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.TabbedPage.SetToolbarPlacement(
            this,
            Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);
#endif
        CurrentPageChanged += (_, _) => RefreshTabs();
        HandlerChanged += (_, _) => RefreshTabs();
        Loaded += (_, _) => RefreshTabs();
    }

    protected override void OnChildAdded(Element child)
    {
        base.OnChildAdded(child);

        if (child is CustomTabPage tabPage)
        {
            UpdateTabIcon(tabPage);
        }

        RefreshTabs();
    }

    protected override void OnChildRemoved(Element child, int oldLogicalIndex)
    {
        base.OnChildRemoved(child, oldLogicalIndex);
        RefreshTabs();
    }

    /// <summary>
    /// Walks through all child tabs updating their icons and notifies the native
    /// tab bar implementation to rebuild its appearance.
    /// </summary>
    public void RefreshTabs()
    {
        foreach (var child in Children.OfType<CustomTabPage>())
        {
            UpdateTabIcon(child);
        }

        RefreshNativeTabs();
    }

    /// <summary>
    /// Updates the IconImageSource of a tab page depending on whether it is the
    /// currently selected tab.
    /// </summary>
    /// <param name="page">The tab page to update.</param>
    private void UpdateTabIcon(CustomTabPage page)
    {
        if (CurrentPage == page)
        {
            if (page.SelectedIcon != null)
            {
                page.IconImageSource = page.SelectedIcon;
            }
        }
        else
        {
            if (page.UnselectedIcon != null)
            {
                page.IconImageSource = page.UnselectedIcon;
            }
        }
    }

    private static void OnTabbedPagePropertyChanged(BindableObject bindable, object? oldValue, object? newValue)
    {
        if (bindable is CustomTabbedPage page)
        {
            page.RefreshTabs();
        }
    }

    /// <summary>
    /// Refreshes the native tab bar implementations. The actual logic lives in
    /// platform specific partial classes generated in CustomTabbedPage.iOS.cs and
    /// CustomTabbedPage.Android.cs.
    /// </summary>
    partial void RefreshNativeTabs();

    #endregion
}
