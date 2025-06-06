using Microsoft.Maui.Controls.Shapes;

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

    [ObservableProperty]
    private bool _isAvailable = true;

    public override ContentView CreateDisplayContent()
{
    // Create a contrasting border color
    Color borderColor = PlateColor == Colors.White || PlateColor == Colors.Yellow 
            || PlateColor.GetLuminosity() > 0.8 ? 
                Colors.Black : Colors.Transparent;

    return new ContentView()
    {
        Content = new VerticalStackLayout()
        {
            new Border
            {
                Stroke = borderColor,
                StrokeThickness = 1,
                StrokeShape = new RoundRectangle { CornerRadius = 2 },
                Padding = 2,
                Content = new VerticalStackLayout()
                {
                    Spacing = 3,
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
                        .Bind(Line.Y2Property,source:this.PlateSize)
                    }
                }
            },

            new Label()
                .Text(KiloGram.ToString())
                .Invoke(label =>
                {
                    if(this.PlateColor == Colors.White || this.PlateColor == Colors.Yellow)
                    {
                        label.TextColor = Colors.Black;
                    }
                    else
                    {
                        label.TextColor = PlateColor;
                    }
                })
        }
    };
}
}
