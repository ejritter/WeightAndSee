namespace WeightAndSee.Models;
public partial class DumbbellModel : BaseModel
{
    public DumbbellModel()
    {
        _barLine.X2 = 30;
        LeftPlates.CollectionChanged += Plates_CollectionChanged;
        RightPlates.CollectionChanged += Plates_CollectionChanged;
    }

    
   //public override View CreateDisplayContent()
   // {
   //     var grid = new Grid()
   //     {
   //         RowSpacing = 0,
   //         ColumnSpacing = 0,
   //         Margin = 0,
   //         Padding = 0
   //     };
        
   //     // Add the bar line first
   //     var barLine = new Line()
   //     {
   //         X1 = 0,
   //         Y1 = 0,
   //         Y2 = 0,
   //         Stroke = Colors.Gray,
   //         StrokeThickness = 8,
   //         HorizontalOptions = LayoutOptions.Center,
   //         VerticalOptions = LayoutOptions.Center,
   //         TranslationY = -10
   //     };
   //     grid.Add(barLine);
        
   //     // Create a grid for plates that allows overlapping
   //     var platesGrid = new Grid()
   //     {
   //         HorizontalOptions = LayoutOptions.Fill,
   //         VerticalOptions = LayoutOptions.Center
   //     };
        
   //     platesGrid.Add(_centerPoint);

   //     for (int i = 0; i < LeftPlates.Count; i++)
   //     {
   //         var plate = LeftPlates[i];
   //         var plateView = plate.DisplayItem();
            
   //         // Position plates from center going left
   //         plateView.HorizontalOptions = LayoutOptions.Center;
   //         plateView.TranslationX = -(_leftOffset + (i * (_plateWidth + _plateSpacing)));
   //         if (plate.KiloGram < 10)
   //         {
   //             plateView.TranslationY = 5;
   //         }
   //         platesGrid.Add(plateView);
   //     }
   //     for (int i = 0; i < RightPlates.Count; i++)
   //     {
   //         var plate = RightPlates[i];
   //         var plateView = plate.DisplayItem();
            
   //         // Position plates from center going right
   //         plateView.HorizontalOptions = LayoutOptions.Center;
   //         plateView.TranslationX = _rightOffset + (i * (_plateWidth + _plateSpacing));
   //         if (plate.KiloGram < 10)
   //         {
   //             plateView.TranslationY = 5;
   //         }
   //         platesGrid.Add(plateView);
   //     }
   //     barLine.X2 = ((LeftPlates.Count() * 2) * _plateWidth) + 200;
   //     grid.Add(platesGrid);
   //     return grid;
   // }

}
