namespace WeightAndSee;

public partial class AppShell : Shell
{
    public AppShell() // Removed MainPage mainPage parameter
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Pages.MainPage), typeof(Pages.MainPage));
    }
}
