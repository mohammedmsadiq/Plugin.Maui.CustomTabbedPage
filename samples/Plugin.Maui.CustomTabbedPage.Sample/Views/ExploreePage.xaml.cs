using Plugin.Maui.CustomTabbedPage.Controls;

namespace Plugin.Maui.CustomTabbedPage.Sample.Views;

public partial class ExploreePage : CustomTabPage
{
    public ExploreePage()
    {
        InitializeComponent();
    }

    private void OnClearBadgeClicked(object sender, EventArgs e)
    {
        BadgeIsVisible = false;
        BadgeText = null;
    }
}
