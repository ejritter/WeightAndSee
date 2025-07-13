
namespace WeightAndSee.Models;
public partial class KiloPlateModel : BaseModel
{
    public KiloPlateModel(IWeightConversionService weightConversionService) : base(weightConversionService)
    {
     
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

    public Color ContrastColor => PlateColor == Colors.White || PlateColor == Colors.Yellow ? 
        Colors.Black : Colors.White;

    public ImageSource PlateImageSource => 
        ImageSource.FromFile($"{KiloPlate.ToLower()}_{CurrentTheme.ToString().ToLower()}.png");
}
