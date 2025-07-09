namespace WeightAndSee.Controls;
public partial class BarTypeRadioControl : RadioButton
{
    public BarTypeRadioControl()
    {
        InitializeControl();
    }

    private void InitializeControl()
    {
        this.HorizontalOptions = LayoutOptions.Center;
        //this.TextColor = Colors.LightSlateGray;
        //this.BorderColor=Colors.Red;
        this.GroupName = "BarType";
        this.FontSize = 18;
    }

}
