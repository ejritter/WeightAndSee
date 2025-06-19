namespace WeightAndSee.Models;

public abstract partial class BaseModel : ObservableObject , IWeightConversionService
{
    [ObservableProperty]
    private double _barWeight; // Assumed to be in pounds

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _leftPlates = new();

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _rightPlates = new();

    [ObservableProperty]
    private string _barType = string.Empty;

    public int TotalWeightInPounds => KilogramToPound(this);

    public double TotalWeightInKilograms => PoundToKilogram(this);

    private const double kgUnitToPounds = 2.20462262815; // Factor to convert KG to Pounds
    private const double poundsToKgUnit = 0.45359237; // Factor to convert Pounds to KG

    protected int _plateSpacing = 10; // Spacing between plates (negative for overlap)
    protected int _plateWidth = 8; // Make plates thin to match new visual
            // Add left plates with overlap (right to left)
    protected int _leftOffset = 50; // Distance from center to first plate
            // Add right plates with overlap (left to right)
    protected int _rightOffset = 50; // Distance from center to first plate

    protected int _plateViewTranslationY = 12;// little plates need to be shifted down to center on bar.
        protected Line _barLine = new Line()
        {
            X1 = 0,
            Y1 = 0,
            Y2 = 0,
            Stroke = Colors.Gray,
            StrokeThickness = 8,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            TranslationY = 0
        };


    //public abstract ContentView CreateItemForDisplay();
    public double PoundToKilogram(BaseModel bar)
    {
        // Convert bar weight (which is in pounds) to kilograms.
        double totalKilograms = bar.BarWeight * poundsToKgUnit;

        // Add weight of plates (which are already in kilograms).
        foreach (KiloPlateModel leftPlate in bar.LeftPlates)
        {
            totalKilograms += leftPlate.KiloGram;
        }
        foreach (KiloPlateModel rightPlate in bar.RightPlates)
        {
            totalKilograms += rightPlate.KiloGram;
        }
        return totalKilograms;
    }

    public int KilogramToPound(BaseModel bar)
    {
        double platesInKg = 0;
        foreach (KiloPlateModel leftPlate in bar.LeftPlates)
        {
            platesInKg += leftPlate.KiloGram;
        }
        foreach (KiloPlateModel rightPlate in bar.RightPlates)
        {
            platesInKg += rightPlate.KiloGram;
        }

        // BarWeight is already in pounds.
        // Convert total plate weight from KG to pounds and add to bar weight.
        return (int)(bar.BarWeight + (platesInKg * kgUnitToPounds));
    }

    protected void Plates_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Rebuild display when plates are added or removed
        RefreshDisplay();
    }
    protected ContentView _displayView;
       // Call this method when you want to force a refresh
       public void ResetBarDisplay()
    {
        _displayView = null;
    }
    public void RefreshDisplay()
    {
        if (_displayView != null)
        {
            _displayView.Content = CreateDisplayContent();
        }
    }

    public virtual View CreateDisplayContent()
    {
        
        var barLine = new Line()
        {
            X1 = _barLine.X1,
            X2 = _barLine.X2,//this is set on barbellDumbell creation
            Y1 = _barLine.Y1,
            Y2 = _barLine.Y2,
            Stroke = _barLine.Stroke,
            StrokeThickness = _barLine.StrokeThickness,
            HorizontalOptions = _barLine.HorizontalOptions,
            VerticalOptions = _barLine.VerticalOptions,
            TranslationY = _barLine.TranslationY
        };
        var centerPoint = new Border
        {
            WidthRequest = 20,
            HeightRequest = 20,
            Stroke = Colors.Transparent, // Change to a color for debugging
            HorizontalOptions = LayoutOptions.Center
        };

        var grid = new Grid()
        {
            RowSpacing = 0,
            ColumnSpacing = 0,
            Margin = 0,
            Padding = 0
        };


        var platesGrid = new Grid()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center,
            Padding = 2,
            Margin = 2,
            ColumnSpacing = 5

        };
        // Add the bar line first
         grid.Add(barLine);
        
        // add plates grid that allows overlapping
         platesGrid.Add(centerPoint);
        

        for (int i = 0; i < LeftPlates.Count; i++)
        {
            var plate = LeftPlates[i];
            var plateView = plate.DisplayItem;
            
            // Position plates from center going left
            plateView.HorizontalOptions = LayoutOptions.Center;
            plateView.TranslationX = -(_leftOffset + (i * (_plateWidth + _plateSpacing)));
            plateView.TranslationY = _plateViewTranslationY;
            
            platesGrid.Add(plateView);
        }
        
        for (int i = 0; i < RightPlates.Count; i++)
        {
            var plate = RightPlates[i];
            var plateView = plate.DisplayItem;
            
            // Position plates from center going right
            plateView.HorizontalOptions = LayoutOptions.Center;
            plateView.TranslationX = _rightOffset + (i * (_plateWidth + _plateSpacing));
            plateView.TranslationY = _plateViewTranslationY;
            
            platesGrid.Add(plateView);
        }                 
        barLine.X2 = ((LeftPlates.Count() * 2) * _plateWidth) + 200;
        grid.Add(platesGrid);
        return grid;
    }
    public ContentView DisplayItem => CreateItemForDisplay();
    public ContentView CreateItemForDisplay()
    {
        if (_displayView == null)
        {
            _displayView = new ContentView
            {
                Content = CreateDisplayContent()
            };
        }
        
        return _displayView;
    }
}
