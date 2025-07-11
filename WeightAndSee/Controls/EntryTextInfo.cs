namespace WeightAndSee.Controls;
public partial class EntryTextInfo : Entry
{
    public EntryTextInfo()
    {
        InitializeControl();
    }

    private void InitializeControl()
    {
        this.HorizontalOptions = LayoutOptions.Center;
        this.Keyboard = Keyboard.Numeric;
        this.FontSize = 22;
        
    }
}
