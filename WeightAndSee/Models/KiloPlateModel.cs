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
    [NotifyPropertyChangedFor(nameof(IsNotAvailable))]
    private bool _isAvailable = true;

    public bool IsNotAvailable => !IsAvailable;

    // Add this computed property
    public bool NeedsBorder => PlateColor == Colors.White || PlateColor == Colors.Yellow;

    // Add this property for border color
    public Color BorderColor => Colors.Black;

    // Add this property for visual contrast
    public Color ContrastColor => PlateColor == Colors.White || PlateColor == Colors.Yellow ? 
        Colors.Black : Colors.White;


}
