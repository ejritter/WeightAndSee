namespace WeightAndSee.Models;
public partial class BarbellModel : BaseModel, IWeightConversionService
{  
    public BarbellModel()
    {
        _barLine.X2 = 200;
        // Don't call CreateItemForDisplay here - it will be called when needed
        // Subscribe to collection changes
        LeftPlates.CollectionChanged += Plates_CollectionChanged;
        RightPlates.CollectionChanged += Plates_CollectionChanged;
    }
    
    
}