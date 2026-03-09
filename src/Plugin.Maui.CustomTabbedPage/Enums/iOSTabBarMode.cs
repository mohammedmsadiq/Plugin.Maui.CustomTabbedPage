namespace Plugin.Maui.CustomTabbedPage;

/// <summary>
/// Defines the different visual modes available for the iOS tab bar.
/// NativeGlass uses the system defined glass-like tab bar appearance,
/// while Branded allows the developer to supply custom colours and opacity.
/// </summary>
public enum iOSTabBarMode
{
    /// <summary>
    /// The default iOS tab bar appearance which adopts the system Liquid Glass look.
    /// </summary>
    NativeGlass = 0,

    /// <summary>
    /// A customisable appearance driven by the plugin where colours and fonts can be specified.
    /// </summary>
    Branded = 1
}