namespace WeightAndSee.Models;

public abstract partial class BaseModel(IWeightConversionService weightConversionService) : ObservableObject
{
    public readonly IWeightConversionService WeightConversionService = weightConversionService;
    [ObservableProperty]
    private double _barWeight; // Assumed to be in pounds

    //public  AppTheme CurrentTheme = App.Current.RequestedTheme;
    [ObservableProperty]
    private AppTheme _currentTheme = App.Current.RequestedTheme;

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _leftPlates = new();

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _rightPlates = new();

    [ObservableProperty]
    private string _barType = string.Empty;
    public int TotalWeightInPounds => WeightConversionService.KilogramToPound(this);
    public double TotalWeightInKilograms => WeightConversionService.PoundToKilogram(this);
    public int PlateWidth = 20; // Make plates thin to match new 
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

    partial void OnCurrentThemeChanged(AppTheme value)
    {
        foreach (var plate in LeftPlates.Concat(RightPlates))
        {
            plate.ThemeChanged();
        }
    }

}
