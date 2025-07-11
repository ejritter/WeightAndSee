namespace WeightAndSee.Services;

public class ViewCreatorService : IViewCreatorService
{
    // Store the current bar model for display
    public BaseModel Bar { get; set; }
    public void Plates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        RefreshDisplay();
    }

    private ContentView _displayView { get; set; } = null;

    public void ResetBarDisplay()
    {
        _displayView = null;
        Bar = null;
    }

    public void RefreshDisplay()
    {
        if (_displayView != null)
        {
            _displayView.Content = CreateDisplayContent();
        }
    }
    public View CreateDisplayContent()
    {
        var barLine = Bar.CloneBarLine();

        //get left plate and right plate count
        var totalPlateWidth = (Bar.LeftPlates.Count * Bar.PlateWidth) + (Bar.RightPlates.Count * Bar.PlateWidth);

        //create bar the lenght of plates and a little more
        var barLength = Bar.BarLine.X2 + totalPlateWidth;
        barLine.X2 = barLength;
        var viewGrid = new Grid
        {

            ColumnDefinitions = Columns.Define(
                (Column.LeftSpace, 20),
                (Column.LeftPlates, totalPlateWidth),
                (Column.Center, 40),
                (Column.RightPlates, totalPlateWidth),
                (Column.RightSpace, 20)),
        };

        viewGrid.Add(barLine, (int)Column.LeftPlates, 0);
        viewGrid.SetColumnSpan(barLine, All<Column>());

        var leftPlates = new HorizontalStackLayout()
        {
            Padding = 2
        };
        var rightPlates = new HorizontalStackLayout()
        {
            Padding = 2
        };

        for (int i = Bar.LeftPlates.Count -1; i >= 0; i--)
        {
            //want to go backwards so it looks like a loaded barbell
            leftPlates.Add(CreatePlateView(Bar.LeftPlates[i]));
        }
        foreach (KiloPlateModel plate in Bar.RightPlates)
        {
            rightPlates.Add(CreatePlateView(plate));
        }

        viewGrid.Add(leftPlates, (int)Column.LeftPlates, 0);
        viewGrid.Add(rightPlates, (int)Column.RightPlates, 0);
        viewGrid.MaximumWidthRequest = barLength + 50;
        return viewGrid;
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

    public View CreatePlateView(KiloPlateModel plate)
    {
        //// Outer border for stroke and padding, mimicking PlatePicker.xaml
        //var outerBorder = new Border
        //{
        //    StrokeShape = new RoundRectangle { CornerRadius = 2 },
        //    Stroke = plate.NeedsBorder ? plate.BorderColor : plate.PlateColor,
        //    StrokeThickness = 1,
        //    Padding = 2,
        //    MinimumWidthRequest = 20,
        //    HorizontalOptions = LayoutOptions.Center
        //};

       // Inner border for the plate itself

       var plateVisual = new Border
       {
           BackgroundColor = plate.PlateColor,
           WidthRequest = 8, // Fixed thickness, as in PlatePicker.xaml
           HeightRequest = plate.PlateSize,
           HorizontalOptions = LayoutOptions.Center,
           Stroke = plate.PlateColor,
           StrokeThickness =  0
       };

        // Label for the weight
        var weightLabel = new Label
        {
            Text = plate.KiloGram.ToString(),
            TextColor = plate.NeedsBorder ? plate.BorderColor : plate.PlateColor,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 12,
            Margin = new Thickness(0, 6, 0, 0)
        };

        // Layout to stack plate and label
        var grid = new Grid
        {
            RowSpacing = 2,
            VerticalOptions = LayoutOptions.Start
        };
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        grid.Add(plateVisual, 0, 0);
        grid.Add(weightLabel, 0, 1);

        return grid;
    }

    private enum Column {LeftSpace, LeftPlates, Center, RightPlates, RightSpace } 
    private enum Row { BarRow }
}
