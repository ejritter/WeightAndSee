namespace WeightAndSee.Models;

public abstract partial class BaseModel(IWeightConversionService weightConversionService) : ObservableObject
{
    protected readonly IWeightConversionService WeightConversionService = weightConversionService;
    [ObservableProperty]
    private double _barWeight; // Assumed to be in pounds

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _leftPlates = new();

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _rightPlates = new();

    [ObservableProperty]
    private string _barType = string.Empty;

    public int TotalWeightInPounds => WeightConversionService.KilogramToPound(this);

    public double TotalWeightInKilograms => WeightConversionService.PoundToKilogram(this);

   // public int PlateSpacing = 22; // Spacing between plates (negative for overlap)
    public int PlateWidth = 20; // Make plates thin to match new visual
            // Add left plates with overlap (right to left)
   // public int LeftOffset = 50; // Distance from center to first plate
            // Add right plates with overlap (left to right)
   // public int RightOffset = 50; // Distance from center to first plate

    public int PlateViewTranslationY = 12;// little plates need to be shifted down to center on bar.

    public Line BarLine { get; set; } = new Line()
    {
        X1 = 0,
        Y1 = 0,
        Y2 = 0,
        Stroke = Colors.Grey,
        StrokeThickness = 8,
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.Center,
        TranslationY = 0
    };

}
