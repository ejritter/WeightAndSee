using Microsoft.Maui.Controls.Shapes;

namespace WeightAndSee.Models;
public partial class BarbellModel : BaseModel, IWeightConversionService
{  
    public BarbellModel()
    {
        _barLine.X2 = 200;
        // Don't call DisplayItem here - it will be called when needed
        // Subscribe to collection changes
        LeftPlates.CollectionChanged += Plates_CollectionChanged;
        RightPlates.CollectionChanged += Plates_CollectionChanged;
    }
    
    // Extracted the content creation to a separate method
    //public override View CreateDisplayContent()
    //{

        
    //    // Add the bar line first
    //     _grid.Add(_barLine);
        
    //    // add plates grid that allows overlapping
    //     _platesGrid.Add(_centerPoint);
        

    //    for (int i = 0; i < LeftPlates.Count; i++)
    //    {
    //        var plate = LeftPlates[i];
    //        var plateView = plate.DisplayItem();
            
    //        // Position plates from center going left
    //        plateView.HorizontalOptions = LayoutOptions.Center;
    //        plateView.TranslationX = -(_leftOffset + (i * (_plateWidth + _plateSpacing)));
    //        if (plate.KiloGram < 10)
    //        {
    //            plateView.TranslationY = 5;
    //        }
    //        _platesGrid.Add(plateView);
    //    }
        

    //    for (int i = 0; i < RightPlates.Count; i++)
    //    {
    //        var plate = RightPlates[i];
    //        var plateView = plate.DisplayItem();
            
    //        // Position plates from center going right
    //        plateView.HorizontalOptions = LayoutOptions.Center;
    //        plateView.TranslationX = _rightOffset + (i * (_plateWidth + _plateSpacing));
    //        if (plate.KiloGram < 10)
    //        {
    //            plateView.TranslationY = 5;
    //        }
    //        _platesGrid.Add(plateView);
    //    }                 
    //    _barLine.X2 = ((LeftPlates.Count() * 2) * _plateWidth) + 200;
    //    _grid.Add(_platesGrid);
    //    return _grid;
    //}
}