using FluentAssertions;
using Plugin.Maui.CustomTabbedPage.Controls;
using Xunit;

namespace Plugin.Maui.CustomTabbedPage.Tests;

/// <summary>
/// Verifies the default values of all bindable properties on <see cref="CustomTabbedPage"/>.
/// </summary>
public class CustomTabbedPageDefaultsTests
{
    [Fact]
    public void iOSModeProperty_DefaultValue_ShouldBeBranded()
    {
        CustomTabbedPage.iOSModeProperty.DefaultValue.Should().Be(iOSTabBarMode.Branded);
    }

    [Fact]
    public void TabBarBackgroundOpacityProperty_DefaultValue_ShouldBe_0_88()
    {
        CustomTabbedPage.TabBarBackgroundOpacityProperty.DefaultValue.Should().Be(0.88d);
    }

    [Fact]
    public void IsTabBarTranslucentProperty_DefaultValue_ShouldBeTrue()
    {
        CustomTabbedPage.IsTabBarTranslucentProperty.DefaultValue.Should().Be(true);
    }

    [Fact]
    public void RemoveTopShadowLineProperty_DefaultValue_ShouldBeTrue()
    {
        CustomTabbedPage.RemoveTopShadowLineProperty.DefaultValue.Should().Be(true);
    }

    [Fact]
    public void TabFontSizeProperty_DefaultValue_ShouldBe_Eleven()
    {
        CustomTabbedPage.TabFontSizeProperty.DefaultValue.Should().Be(11d);
    }

    [Fact]
    public void TabFontFamilyProperty_DefaultValue_ShouldBeNull()
    {
        CustomTabbedPage.TabFontFamilyProperty.DefaultValue.Should().BeNull();
    }

    [Fact]
    public void iOSModeProperty_PropertyName_ShouldMatch()
    {
        CustomTabbedPage.iOSModeProperty.PropertyName.Should().Be(nameof(CustomTabbedPage.iOSMode));
    }

    [Fact]
    public void TabBarSelectedColorProperty_PropertyName_ShouldMatch()
    {
        CustomTabbedPage.TabBarSelectedColorProperty.PropertyName.Should().Be(nameof(CustomTabbedPage.TabBarSelectedColor));
    }

    [Fact]
    public void TabBarUnselectedColorProperty_PropertyName_ShouldMatch()
    {
        CustomTabbedPage.TabBarUnselectedColorProperty.PropertyName.Should().Be(nameof(CustomTabbedPage.TabBarUnselectedColor));
    }

    [Fact]
    public void TabBarShadowColorProperty_PropertyName_ShouldMatch()
    {
        CustomTabbedPage.TabBarShadowColorProperty.PropertyName.Should().Be(nameof(CustomTabbedPage.TabBarShadowColor));
    }
}
