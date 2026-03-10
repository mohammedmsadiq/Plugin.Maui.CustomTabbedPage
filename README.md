# Plugin.Maui.CustomTabbedPage

A lightweight yet flexible tab control for .NET MAUI that lets you host real `ContentPage` children while fully customising the native tab bar on iOS and Android. Control per-tab icons, badges, and text visibility, as well as global bar colours, opacity, and iOS appearance mode — all from XAML or C#.

[![Build](https://github.com/YOUR_ORG/Plugin.Maui.CustomTabbedPage/actions/workflows/build.yml/badge.svg)](https://github.com/YOUR_ORG/Plugin.Maui.CustomTabbedPage/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.CustomTabbedPage.svg)](https://www.nuget.org/packages/Plugin.Maui.CustomTabbedPage)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Supported Platforms

| Platform | Supported |
|----------|-----------|
| iOS      | ✅ |
| Android  | ✅ |

## Installation

Install from NuGet:

```bash
dotnet add package Plugin.Maui.CustomTabbedPage
```

Or search for `Plugin.Maui.CustomTabbedPage` in the NuGet package manager inside Visual Studio / Rider.

## Getting Started

### 1. Register the plugin

In `MauiProgram.cs`, call `UseCustomTabbedPage()` to register the native mappers:

```csharp
using Plugin.Maui.CustomTabbedPage.Extensions;

public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();

    builder
        .UseMauiApp<App>()
        .UseCustomTabbedPage();

    return builder.Build();
}
```

### 2. Create your tab pages

Each tab must derive from `CustomTabPage` instead of `ContentPage`:

```csharp
using Plugin.Maui.CustomTabbedPage.Controls;

public partial class HomePage : CustomTabPage
{
    public HomePage() => InitializeComponent();
}
```

Or in XAML:

```xml
<controls:CustomTabPage
    xmlns:controls="clr-namespace:Plugin.Maui.CustomTabbedPage.Controls;assembly=Plugin.Maui.CustomTabbedPage"
    x:Class="MyApp.Views.HomePage"
    Title="Home">
    <!-- your content -->
</controls:CustomTabPage>
```

### 3. Build your tabbed shell

Use `CustomTabbedPage` as your root page and add `CustomTabPage` children:

```xml
<controls:CustomTabbedPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:controls="clr-namespace:Plugin.Maui.CustomTabbedPage.Controls;assembly=Plugin.Maui.CustomTabbedPage"
    xmlns:views="clr-namespace:MyApp.Views"
    x:Class="MyApp.MainTabbedPage"
    TabBarSelectedColor="#512BD4"
    TabBarUnselectedColor="#9E9E9E"
    TabBarBackgroundColor="#FFFFFF"
    TabBarBackgroundOpacity="1.0"
    RemoveTopShadowLine="True"
    iOSMode="Branded">

    <views:HomePage
        Title="Home"
        ShowText="True"
        BadgeIsVisible="False" />

    <views:NotificationsPage
        Title="Alerts"
        ShowText="True"
        BadgeIsVisible="True"
        BadgeText="3"
        TabBadgeColor="#FF3B30"
        TabBadgeTextColor="White" />

    <views:ProfilePage
        Title="Profile"
        ShowText="True" />

</controls:CustomTabbedPage>
```

Set `App.MainPage` to an instance of your tabbed page:

```csharp
public partial class App : Application
{
    public App() => MainPage = new MainTabbedPage();
}
```

---

## API Reference

### `CustomTabbedPage`

The top-level container. Inherits from `TabbedPage`.

#### Tab bar appearance

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `iOSMode` | `iOSTabBarMode` | `Branded` | Visual mode for the iOS tab bar (`NativeGlass` or `Branded`). |
| `TabBarBackgroundColor` | `Color` | `Transparent` | Background colour of the tab bar (iOS Branded mode). |
| `TabBarBackgroundOpacity` | `double` | `0.88` | Opacity applied to the tab bar background. |
| `IsTabBarTranslucent` | `bool` | `true` | Whether the iOS tab bar is translucent. |
| `TabBarSelectedColor` | `Color` | `Black` | Colour of selected tab icons and titles. |
| `TabBarUnselectedColor` | `Color` | `Gray` | Colour of unselected tab icons and titles. |
| `TabBarShadowColor` | `Color` | `Transparent` | Colour of the separator line above the tab bar. |
| `RemoveTopShadowLine` | `bool` | `true` | Removes the separator line above the tab bar when `true`. |
| `TabFontFamily` | `string?` | `null` | Global font family for tab titles (overridden per-tab). |
| `TabFontSize` | `double` | `11` | Global font size for tab titles (overridden per-tab). |

#### Methods

| Method | Description |
|--------|-------------|
| `RefreshTabs()` | Forces the native tab bar to re-read all tab properties and redraw. Call this after programmatically changing tab properties outside of bindings. |

---

### `CustomTabPage`

The base class for individual tab pages. Inherits from `ContentPage`.

#### Per-tab properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `SelectedIcon` | `ImageSource?` | `null` | Icon displayed when this tab is selected. Falls back to `IconImageSource` if `null`. |
| `UnselectedIcon` | `ImageSource?` | `null` | Icon displayed when this tab is not selected. Falls back to `IconImageSource` if `null`. |
| `ShowText` | `bool` | `true` | Whether the tab title is displayed in the tab bar. |
| `BadgeIsVisible` | `bool` | `false` | Shows or hides the badge. |
| `BadgeText` | `string?` | `null` | Text shown inside the badge. |
| `TabBadgeColor` | `Color` | `Red` | Background colour of the badge. |
| `TabBadgeTextColor` | `Color` | `White` | Text colour of the badge. |
| `TabFontFamily` | `string?` | `null` | Per-tab font family override for the tab title. |
| `TabFontSize` | `double` | `11` | Per-tab font size override for the tab title. |

---

### `iOSTabBarMode` enum

| Value | Description |
|-------|-------------|
| `NativeGlass` | Uses the system Liquid Glass tab bar appearance introduced in iOS 18. |
| `Branded` | Uses a fully customisable appearance driven by the plugin properties. |

---

## Advanced Usage

### Dynamic badges

Update `BadgeText` and `BadgeIsVisible` at any time — the tab bar refreshes automatically via property-change callbacks:

```csharp
// Show a badge with count
myAlertsTab.BadgeText = unreadCount.ToString();
myAlertsTab.BadgeIsVisible = unreadCount > 0;
```

### Per-tab selected / unselected icons

```xml
<views:HomePage
    Title="Home"
    SelectedIcon="home_selected.png"
    UnselectedIcon="home_outline.png" />
```

### Hiding tab titles (icon-only tabs)

```xml
<views:HomePage Title="Home" ShowText="False" />
```

### NativeGlass mode (iOS 18+)

Use `iOSMode="NativeGlass"` to adopt the system's translucent Liquid Glass look. In this mode the colour customisation properties have no effect.

```xml
<controls:CustomTabbedPage iOSMode="NativeGlass" ...>
```

---

## Building from Source

1. Clone the repository.
2. Install the .NET 9 SDK and MAUI workload:
   ```bash
   dotnet workload install maui
   ```
3. Open `Plugin.Maui.CustomTabbedPage.sln` or build from the command line:
   ```bash
   dotnet build src/Plugin.Maui.CustomTabbedPage
   ```
4. Run the unit tests (no device required):
   ```bash
   dotnet test tests/Plugin.Maui.CustomTabbedPage.Tests
   ```
5. Run the sample app from Visual Studio / Rider, targeting an iOS simulator or Android emulator.

---

## Known Limitations

- MacCatalyst and Windows are not yet supported — `CustomTabbedPage` will fall back to the default MAUI `TabbedPage` behaviour on those platforms.
- Advanced layouts such as floating action buttons, animated tab indicators, or fully bespoke tab bar views are outside the scope of this library but can be built on top of the architecture provided.
- `iOSMode="NativeGlass"` requires iOS 18 or later; on earlier versions it falls back to the system default appearance.

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) and [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) before opening a pull request.

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a full list of changes.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
