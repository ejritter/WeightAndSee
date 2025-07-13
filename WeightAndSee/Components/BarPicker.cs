namespace WeightAndSee.Components;

public partial class BarPicker : ContentView
{
    public BarPicker()
    {
        _myList = new ListView();
        InitializeComponent();
        // The Picker's ItemsSource will be set from the ViewModel in XAML.
        // The EventToCommandBehavior is now in XAML.

    }

    public ListView _myList;

    [RelayCommand]
    private void Random()
    {

    }
}
