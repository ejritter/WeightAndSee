namespace WeightAndSee.Views;
public class AvailablePlatesView : DataTemplate
{
    public AvailablePlatesView() : base(() => DisplayAvailablePlates().Top())
    {

    }

    private static HorizontalStackLayout DisplayAvailablePlates() => new HorizontalStackLayout()
    {
        Spacing = 4,
        Margin = 4,
        Padding = 4,

        Children =
        {
            new ContentView()
            .Bind(ContentView.ContentProperty, getter:(KiloPlateModel kp) => kp.DisplayItem)
            .Bind(ContentView.HeightProperty, getter:(KiloPlateModel kp) => kp.PlateSize)
        }
    };
}
