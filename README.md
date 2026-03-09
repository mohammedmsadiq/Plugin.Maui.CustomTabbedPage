# Plugin.Maui.CustomTabbedPage

`Plugin.Maui.CustomTabbedPage` is a lightweight yet flexible tab control for .NET MAUI which allows you to host real `ContentPage` children while customizing the look and feel of the native tab bar on both iOS and Android. It provides properties for controlling per–tab icons, badges and text visibility along with global appearance settings such as selected/unselected colours, background opacity and a choice of native or custom styling on iOS.

## Getting Started

1. **Install the NuGet package** (not yet published) or reference the project directly from source.
2. In your `MauiProgram.cs` call `builder.UseCustomTabbedPage()` to register the custom control:

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();

    builder
        .UseMauiApp<App>()
        .UseCustomTabbedPage();

    return builder.Build();
}
```

3. Use `CustomTabbedPage` as the container for your tabs and derive each tab from `CustomTabPage`. Set the various properties to customise the appearance:

```xml
<custom:CustomTabbedPage
    xmlns:custom="clr-namespace:Plugin.Maui.CustomTabbedPage.Controls"
    TabBarSelectedColor="Black"
    TabBarUnselectedColor="Gray"
    TabBarBackgroundColor="#F7F7F7"
    iOSMode="Branded">

    <views:HomePage
        Title="Home"
        ShowText="True"
        BadgeIsVisible="True"
        BadgeText="3" />

    <views:SettingsPage
        Title="Settings"
        ShowText="False" />

</custom:CustomTabbedPage>
```

## Features

- Host true `ContentPage` instances rather than `View` objects.
- Per–tab selected/unselected icons.
- Per–tab badges with custom colours and text.
- Control visibility of tab titles individually.
- Global iOS appearance settings including custom background, opacity, selected/unselected colours and the option to remove the top shadow line.
- Android bottom navigation support using the native `BottomNavigationView`.

## Limitations

This library intentionally stays close to the native platform controls. Some advanced scenarios such as floating action buttons, animated indicators or fully bespoke layouts are not implemented but can be built on top of the architecture provided here.

## License

This project is provided under the MIT license. See `LICENSE` for details.