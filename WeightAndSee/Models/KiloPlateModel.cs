
namespace WeightAndSee.Models;
public partial class KiloPlateModel : BaseModel
{
    public KiloPlateModel(IWeightConversionService weightConversionService) : base(weightConversionService)
    {
        PlateTextColor = GetPlateTextColor();
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
    [NotifyPropertyChangedFor(nameof(IsNotAvailable))]
    [NotifyPropertyChangedFor(nameof(PlateOpacity))]
    private bool _isAvailable = true;

    public bool IsNotAvailable => !IsAvailable;

    public double PlateOpacity => IsAvailable ? 1.0 : 0.2;

    public bool NeedsBorder => CurrentTheme == AppTheme.Light &&
            PlateColor == Colors.White;

    public Color BorderColor => Colors.Black;


    public Color PlateTextColor
    {
        get => _plateTextColor;
        set => _plateTextColor = GetPlateTextColor();
    }

    private Color _plateTextColor;

    private Color GetPlateTextColor()
    {
        if (CurrentTheme == AppTheme.Dark && PlateColor == Colors.Black)
        {
            return Colors.White;
        }
        if (CurrentTheme == AppTheme.Light && PlateColor == Colors.White)
        {
            return Colors.Black;
        }

        return PlateColor;
    }

    public void ThemeChanged()
    {
        OnPropertyChanged(nameof(PlateTextColor));
    }

    public ImageSource PlateImageSource =>
        ImageSource.FromFile($"{KiloPlate.ToLower()}_{CurrentTheme.ToString().ToLower()}.png");
}
