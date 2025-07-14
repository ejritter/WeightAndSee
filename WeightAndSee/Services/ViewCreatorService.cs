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

    public void SetViewBar(BaseModel bar)
    {
        Bar = bar;
        if (_displayView != null)
        {
            _displayView.Content = CreateDisplayContent();
        }
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

        // Calculate plate dimensions
        var totalLeftPlateWidth = Bar.LeftPlates.Count * Bar.PlateWidth;
        var totalRightPlateWidth = Bar.RightPlates.Count * Bar.PlateWidth;
        
        // Define bar sections
        var sleeveLength = 80;  // Length of each sleeve (where plates go)
        var centerGripLength = 120; // Center grip section
        var minSleeveLength = Math.Max(sleeveLength, Math.Max(totalLeftPlateWidth + 20, totalRightPlateWidth + 20)); // Ensure sleeves can fit all plates
        
        // Calculate total bar length dynamically
        var totalBarLength = (minSleeveLength * 2) + centerGripLength;
        barLine.X2 = totalBarLength;
        
        var viewGrid = new Grid
        {
            ColumnDefinitions = Columns.Define(
                (Column.LeftSpace, 10), 
                (Column.LeftPlates, minSleeveLength), 
                (Column.Center, centerGripLength), 
                (Column.RightPlates, minSleeveLength), 
                (Column.RightSpace, 10)), 
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        
        viewGrid.Add(barLine, (int)Column.LeftSpace, 0);
        viewGrid.SetColumnSpan(barLine, All<Column>());

        
        var leftPlates = new HorizontalStackLayout()
        {
            Padding = 2,
            HorizontalOptions = LayoutOptions.End, 
            VerticalOptions = LayoutOptions.Center,
            Spacing = 0
        };
        
        var rightPlates = new HorizontalStackLayout()
        {
            Padding = 2, 
            HorizontalOptions = LayoutOptions.Start, 
            VerticalOptions = LayoutOptions.Center,
            Spacing = 0 
        };

        
        for (int i = Bar.LeftPlates.Count - 1; i >= 0; i--)
        {
            leftPlates.Add(CreatePlateOnBarView(Bar.LeftPlates[i]));
        }
        
        
        foreach (KiloPlateModel plate in Bar.RightPlates)
        {
            rightPlates.Add(CreatePlateOnBarView(plate));
        }

        
        viewGrid.Add(leftPlates, (int)Column.LeftPlates, 0);
        viewGrid.Add(rightPlates, (int)Column.RightPlates, 0);
        
        
        viewGrid.MaximumWidthRequest = totalBarLength + 50;
        
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

    public ContentView BarReportView(BaseModel bar)
   
    {
        VerticalStackLayout verticalStackLayout = new()
        {
            Margin =  5,
            Spacing = 5,
            Padding = 5
        };
        var plateCount = 0;
        var uniquePlates = bar.LeftPlates
                                .Concat(bar.RightPlates)
                                .DistinctBy(p => p.KiloPlate);
        //return vertical stack layout labels.
        //need one label for the Your Bar Needs:
        //then each new label from here should change the text color of the KiloGram Plate.
        //we need to make a horizontalGroup for the labels so something like:
        //<verticalGroup>
        //one for each label => <horizontalGroup>
        //</verticalGroup>

        List<HorizontalStackLayout> plateCountLabels = new();
        plateCountLabels.Add(new HorizontalStackLayout()
        {
            Padding = 5,
            Margin = 5,
            Spacing =5,
            Children =
                {
                    new EntryLabelInfo()
                        .Text($"Your {Bar.BarType} needs:")
                }
        });

        foreach (KiloPlateModel plate in uniquePlates)
        {
           plateCount += bar.LeftPlates.Concat(bar.RightPlates)
                                       .Count(p => p.KiloPlate == plate.KiloPlate);

            var plateDetailLabel = new EntryLabelInfo()
            {
                FormattedText = new FormattedString
                {
                     Spans =
                     {
                         new Span { Text = $"\t{plateCount}", TextColor = plate.PlateColor},
                         new Span { Text = " of " },
                         new Span { Text = $"{plate.KiloGram}.", TextColor = plate.PlateColor}
                     }
                }
            };


            var newHorizontalGroup = new HorizontalStackLayout()
            {
                Padding = 5,
                Margin = 5,
                Spacing = 5,
                Children =
                    {
                        plateDetailLabel,
                        CreateSinglePlateView(plate)
                    }
            };
            plateCountLabels.Add(newHorizontalGroup);
            plateCount = 0;//reset for next plate.
    }
    plateCountLabels.Add(new HorizontalStackLayout()
    {
        Padding = 3,
                Margin = 3,
                Spacing = 3,
                Children =
                {
            new EntryLabelInfo()
                .Text($"For a total weight of {bar.TotalWeightInPounds} pounds.")
                }
    });
        foreach (HorizontalStackLayout stack in plateCountLabels)
        {

            verticalStackLayout.Add(stack);
        }
        return new ContentView()
        {
            Content = verticalStackLayout
        };
    }

    public Image CreateSinglePlateView(KiloPlateModel plate)
    {
        return new Image
        {
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions= LayoutOptions.Center,
                WidthRequest= plate.PlateSize,
                HeightRequest=plate.PlateSize,
                Opacity=plate.PlateOpacity,
                Source= plate.PlateImageSource
        };
    }
    public View CreatePlateOnBarView(KiloPlateModel plate)
    {

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
            VerticalOptions = LayoutOptions.Center
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
