namespace WeightAndSee.Models;
public partial class KiloPlateModel: BaseModel
{
    public KiloPlateModel()
    {
        LeftPlates.CollectionChanged += Plates_CollectionChanged;
        RightPlates.CollectionChanged += Plates_CollectionChanged;
    }
    [ObservableProperty]
    private double _kiloGram;

    [ObservableProperty]
    private string _KiloPlate;

    [ObservableProperty]
    private Color _plateColor;

    [ObservableProperty]
    private double _plateSize;
    
    public override View CreateDisplayContent()
    {
        return new ContentView()
        {
            Content = new StackLayout()
            {
                Children =
                {
                    new Line()
                    {
                        X1 = 0,
                        X2 = 0,
                        Y1 = 0,
                        Stroke = PlateColor,
                        StrokeThickness = 8
                    }
                    .Bind(Line.Y2Property,source:this.PlateSize),
                    new Label()
                       .Bind(Label.TextProperty, source:this.KiloGram)
                }
            }
        };
    }
}
