namespace WeightAndSee;

public class AppShell : Shell
{
    public AppShell(MainPage mainPage)
    {
        Items.Add(mainPage);
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
