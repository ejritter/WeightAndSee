namespace WeightAndSee;

public class App : Application
{
    private Shell _shell { get; init; }
    public App(AppShell shell)
    {
        _shell = shell;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(_shell)
        {

        };

        return window;
    }
}
