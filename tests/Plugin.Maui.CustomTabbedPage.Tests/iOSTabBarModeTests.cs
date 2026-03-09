using FluentAssertions;
using Plugin.Maui.CustomTabbedPage;
using Xunit;

namespace Plugin.Maui.CustomTabbedPage.Tests;

/// <summary>
/// Tests for the <see cref="iOSTabBarMode"/> enum.
/// </summary>
public class iOSTabBarModeTests
{
    [Fact]
    public void NativeGlass_ShouldHaveValue_Zero()
    {
        ((int)iOSTabBarMode.NativeGlass).Should().Be(0);
    }

    [Fact]
    public void Branded_ShouldHaveValue_One()
    {
        ((int)iOSTabBarMode.Branded).Should().Be(1);
    }

    [Fact]
    public void Enum_ShouldContainExactlyTwoValues()
    {
        var values = Enum.GetValues<iOSTabBarMode>();
        values.Should().HaveCount(2);
    }

    [Theory]
    [InlineData(0, iOSTabBarMode.NativeGlass)]
    [InlineData(1, iOSTabBarMode.Branded)]
    public void CastFromInt_ShouldReturnCorrectMode(int value, iOSTabBarMode expected)
    {
        ((iOSTabBarMode)value).Should().Be(expected);
    }
}
