namespace WeightAndSee.Services;

public class ViewCreatorService : IViewCreatorService
{
    // Store the current bar model for display
    public BaseModel Bar { get; set; }
    public void Plates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        RefreshDisplay();
    }

    public ContentView DisplayView { get; set; } = null;

    public void ResetBarDisplay()
    {
        DisplayView = null;
        Bar = null;
    }

    public void RefreshDisplay()
    {
        if (DisplayView != null)
        {
            DisplayView.Content = CreateDisplayContent();
        }
    }
    public View CreateDisplayContent()
    {

        var barLine = new Line()
        {
            X1 = Bar.BarLine.X1,
            X2 = Bar.BarLine.X2,//this is set on barbellDumbell creation
            Y1 = Bar.BarLine.Y1,
            Y2 = Bar.BarLine.Y2,
            Stroke = Bar.BarLine.Stroke,
            StrokeThickness = Bar.BarLine.StrokeThickness,
            HorizontalOptions = Bar.BarLine.HorizontalOptions,
            VerticalOptions = Bar.BarLine.VerticalOptions,
            TranslationY = Bar.BarLine.TranslationY
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


        for (int i = 0; i < Bar.LeftPlates.Count; i++)
        {
            var plate = Bar.LeftPlates[i];
            var plateView = CreatePlateView(plate);

            // Position plates from center going left
            plateView.HorizontalOptions = LayoutOptions.Center;
            plateView.TranslationX = -(Bar.LeftOffset + (i * (Bar.PlateWidth + Bar.PlateSpacing)));
            plateView.TranslationY = Bar.PlateViewTranslationY;

            platesGrid.Add(plateView);
        }

        for (int i = 0; i < Bar.RightPlates.Count; i++)
        {
            var plate = Bar.RightPlates[i];
            var plateView = CreatePlateView(plate);

            // Position plates from center going right
            plateView.HorizontalOptions = LayoutOptions.Center;
            plateView.TranslationX = Bar.LeftOffset + (i * (Bar.PlateWidth + Bar.PlateSpacing));
            plateView.TranslationY = Bar.PlateViewTranslationY;

            platesGrid.Add(plateView);
        }
        barLine.X2 = ((Bar.LeftPlates.Count() * 2) * Bar.PlateWidth) + 200;
        grid.Add(platesGrid);
        return grid;
    }


    public ContentView DisplayItem => CreateItemForDisplay();

    public ContentView CreateItemForDisplay()
    {
        if (DisplayView == null)
        {
            DisplayView = new ContentView
            {
                Content = CreateDisplayContent()
            };
        }
        return DisplayView;
    }

    public View CreatePlateView(KiloPlateModel plate)
    {
        var plateContainer = new VerticalStackLayout
        {
            Spacing = 1,
            HorizontalOptions = LayoutOptions.Center
        };

        var plateVisual = new Border
        {
            HeightRequest = plate.PlateSize,
            WidthRequest = plate.PlateWidth,
            BackgroundColor = plate.PlateColor,
            StrokeShape = new RoundRectangle { CornerRadius = 2 },
            Stroke = plate.NeedsBorder ? plate.BorderColor : plate.PlateColor,
            StrokeThickness = 1
        };

        var weightLabel = new Label
        {
            Text = plate.KiloGram.ToString(),
            TextColor = plate.NeedsBorder ? plate.BorderColor : plate.PlateColor,
            FontSize = 10,
            HorizontalTextAlignment = TextAlignment.Center
        };

        plateContainer.Children.Add(plateVisual);
        plateContainer.Children.Add(weightLabel);

        return plateContainer;
    }
}
