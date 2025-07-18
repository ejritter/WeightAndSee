namespace WeightAndSee.Controls;
public partial class EntryLabelInfo : Label
{
    public EntryLabelInfo()
    {
        InitializeControl();
    }

    private void InitializeControl()
    {
        this.HorizontalOptions = LayoutOptions.Start;
        this.VerticalOptions = LayoutOptions.Start;
        this.FontSize = 22;
        this.FontAttributes = FontAttributes.Bold;
    }
}
