using FluentAssertions;
using Plugin.Maui.CustomTabbedPage.Controls;
using Xunit;

namespace Plugin.Maui.CustomTabbedPage.Tests;

/// <summary>
/// Verifies the default values of all bindable properties on <see cref="CustomTabPage"/>.
/// These tests run on plain net8.0 (no MAUI runtime) and rely only on the static
/// BindableProperty definitions — they do not instantiate MAUI objects.
/// </summary>
public class CustomTabPageDefaultsTests
{
    [Fact]
    public void BadgeTextProperty_DefaultValue_ShouldBeNull()
    {
        CustomTabPage.BadgeTextProperty.DefaultValue.Should().BeNull();
    }

    [Fact]
    public void BadgeIsVisibleProperty_DefaultValue_ShouldBeFalse()
    {
        CustomTabPage.BadgeIsVisibleProperty.DefaultValue.Should().Be(false);
    }

    [Fact]
    public void ShowTextProperty_DefaultValue_ShouldBeTrue()
    {
        CustomTabPage.ShowTextProperty.DefaultValue.Should().Be(true);
    }

    [Fact]
    public void TabFontSizeProperty_DefaultValue_ShouldBe_Eleven()
    {
        CustomTabPage.TabFontSizeProperty.DefaultValue.Should().Be(11d);
    }

    [Fact]
    public void SelectedIconProperty_DefaultValue_ShouldBeNull()
    {
        CustomTabPage.SelectedIconProperty.DefaultValue.Should().BeNull();
    }

    [Fact]
    public void UnselectedIconProperty_DefaultValue_ShouldBeNull()
    {
        CustomTabPage.UnselectedIconProperty.DefaultValue.Should().BeNull();
    }

    [Fact]
    public void TabFontFamilyProperty_DefaultValue_ShouldBeNull()
    {
        CustomTabPage.TabFontFamilyProperty.DefaultValue.Should().BeNull();
    }
}
