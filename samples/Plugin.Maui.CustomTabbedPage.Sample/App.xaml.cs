namespace Plugin.Maui.CustomTabbedPage.Sample;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainTabbedPage();
    }
}
