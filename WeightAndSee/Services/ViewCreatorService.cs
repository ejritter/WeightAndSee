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
        //viewGrid.Add(new BoxView { WidthRequest = 40 }, (int)Column.Center, 0);
        return viewGrid;
    }
    //public View CreateDisplayContent()
    //{
    //    int leftCount = Bar.LeftPlates.Count;
    //    int rightCount = Bar.RightPlates.Count;
    //    int plateCount = leftCount + rightCount;

    //    double maxPlateHeight = 40;
    //    if (leftCount > 0)
    //    { 
    //        maxPlateHeight = Math.Max(maxPlateHeight, Bar.LeftPlates.Max(p => p.PlateSize)); 
    //    }
    //    if (rightCount > 0)
    //    {
    //        maxPlateHeight = Math.Max(maxPlateHeight, Bar.RightPlates.Max(p => p.PlateSize));
    //    }
    //    double barHeight = maxPlateHeight + 30;

    //    // Calculate total width for all plates and spacing
    //    double leftWidth = leftCount * Bar.PlateWidth + (leftCount > 0 ? (leftCount - 1) * Bar.PlateSpacing : 0) + Bar.LeftOffset;
    //    double rightWidth = rightCount * Bar.PlateWidth + (rightCount > 0 ? (rightCount - 1) * Bar.PlateSpacing : 0) + Bar.RightOffset;
    //    double totalWidth = leftWidth + rightWidth + 40; // +40 for center marker and margin

    //    // Main grid: 3 columns (left plates, center, right plates)
    //    var grid = new Grid
    //    {
    //        HeightRequest = barHeight,
    //        WidthRequest = totalWidth,
    //        ColumnDefinitions = Columns.Define(
    //            (Column.LeftPlates, Star),
    //            (Column.Center, 40),
    //            (Column.RightPlates, Star))
    //    //{
    //    //    new ColumnDefinition { Width = new GridLength(leftWidth, GridUnitType.Absolute) },
    //    //    new ColumnDefinition { Width = new GridLength(40, GridUnitType.Absolute) }, // center marker
    //    //    new ColumnDefinition { Width = new GridLength(rightWidth, GridUnitType.Absolute) }
    //    //}
    //    };


    //    //var barLine = new BoxView
    //    //{
    //    //    HeightRequest = 8,
    //    //    WidthRequest = totalWidth,
    //    //    Color = Colors.Grey,
    //    //    HorizontalOptions = LayoutOptions.Fill,
    //    //    VerticalOptions = LayoutOptions.Center
    //    //};
    //    var barLine = Bar.BarLine;
    //    grid.Add(barLine, (int)Column.LeftPlates, 0);
    //    Grid.SetColumnSpan(barLine, 3);

    //    // Left plates (right-aligned)
    //    var leftStack = new HorizontalStackLayout
    //    {
    //        Spacing = Bar.PlateSpacing,
    //        VerticalOptions = LayoutOptions.Center,
    //        HorizontalOptions = LayoutOptions.Start
    //    };
    //    for (int i = leftCount - 1; i >= 0; i--)
    //    {
    //        leftStack.Children.Add(CreatePlateView(Bar.LeftPlates[i]));
    //    }            
    //    grid.Add(leftStack, 0, 0);

    //    // Center marker (optional)
    //    var centerMarker = new BoxView
    //    {
    //        WidthRequest = 4,
    //        HeightRequest = maxPlateHeight,
    //        Color = Colors.Transparent,
    //        HorizontalOptions = LayoutOptions.Center,
    //        VerticalOptions = LayoutOptions.Center
    //    };
    //    grid.Add(centerMarker, 1, 0);

    //    // Right plates (left-aligned)
    //    var rightStack = new HorizontalStackLayout
    //    {
    //        Spacing = Bar.PlateSpacing,
    //        VerticalOptions = LayoutOptions.Center,
    //        HorizontalOptions = LayoutOptions.End,
    //    };
    //    for (int i = 0; i < rightCount; i++)
    //    {
    //        rightStack.Children.Add(CreatePlateView(Bar.RightPlates[i]));
    //    }

    //    grid.Add(rightStack, 2, 0);

    //    // Scroll if needed
    //    var scrollView = new ScrollView
    //    {
    //        Orientation = ScrollOrientation.Horizontal,
    //        Content = grid
    //    };

    //    return scrollView;
    //}


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
        // Outer border for stroke and padding, mimicking PlatePicker.xaml
        var outerBorder = new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 2 },
            Stroke = plate.NeedsBorder ? plate.BorderColor : plate.PlateColor,
            StrokeThickness = 1,
            Padding = 2,
            MinimumWidthRequest = 20,
            HorizontalOptions = LayoutOptions.Center
        };

        // Inner border for the plate itself
        var plateVisual = new Border
        {
            BackgroundColor = plate.PlateColor,
            WidthRequest = 8, // Fixed thickness, as in PlatePicker.xaml
            HeightRequest = plate.PlateSize,
            HorizontalOptions = LayoutOptions.Center,
            Stroke = plate.NeedsBorder ? plate.BorderColor : plate.PlateColor,
            StrokeThickness = plate.NeedsBorder ? 1 : 0
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

        outerBorder.Content = grid;
        return outerBorder;
    }

    private enum Column {LeftSpace, LeftPlates, Center, RightPlates, RightSpace } 
    private enum Row { BarRow }
}
