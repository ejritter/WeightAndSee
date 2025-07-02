using Microsoft.Maui.Controls.Shapes;

namespace WeightAndSee.Models;
public partial class KiloPlateModel : BaseModel
{
    public KiloPlateModel(IWeightConversionService weightConversionService) : base(weightConversionService)
    {
        //LeftPlates.CollectionChanged += Plates_CollectionChanged;
        //RightPlates.CollectionChanged += Plates_CollectionChanged;
    }

    [ObservableProperty]
    private double _kiloGram;

    [ObservableProperty]
    private string _KiloPlate;

    [ObservableProperty]
    private Color _plateColor;

    [ObservableProperty]
    private double _plateSize;

    [ObservableProperty]
    private bool _isAvailable = true;

    // Add this computed property
    public bool NeedsBorder => PlateColor == Colors.White || PlateColor == Colors.Yellow;

    // Add this property for border color
    public Color BorderColor => Colors.Black;

    // Add this property for visual contrast
    public Color ContrastColor => PlateColor == Colors.White || PlateColor == Colors.Yellow ? 
        Colors.Black : Colors.White;

    //public override View CreateDisplayContent()
    //{
    //    var plateContainer = new VerticalStackLayout
    //    {
    //        Spacing = 1,
    //        HorizontalOptions = LayoutOptions.Center
    //    };

    //    var plateVisual = new Border
    //    {
    //        HeightRequest = PlateSize,
    //        WidthRequest = _plateWidth,
    //        BackgroundColor = PlateColor,
    //        StrokeShape = new RoundRectangle { CornerRadius = 2 },
    //        Stroke = NeedsBorder ? BorderColor : PlateColor,
    //        StrokeThickness = 1
    //    };

    //    var weightLabel = new Label
    //    {
    //        Text = KiloGram.ToString(),
    //        TextColor = NeedsBorder ? BorderColor : PlateColor,
    //        FontSize = 10,
    //        HorizontalTextAlignment = TextAlignment.Center
    //    };

    //    plateContainer.Children.Add(plateVisual);
    //    plateContainer.Children.Add(weightLabel);

    //    return plateContainer;
    //}
}
