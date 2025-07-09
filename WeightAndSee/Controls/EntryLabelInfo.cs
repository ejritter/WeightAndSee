namespace WeightAndSee.Controls;
public partial class EntryLabelInfo : Label
{
    public EntryLabelInfo()
    {
        InitializeControl();
    }

    private void InitializeControl()
    {
        // Set default properties for the label
        this.HorizontalOptions = LayoutOptions.Start;
        this.VerticalOptions = LayoutOptions.Start;
        this.FontSize = 22;
        this.TextColor = Colors.LightSlateGray;
        this.FontAttributes = FontAttributes.Bold;
    }
}
