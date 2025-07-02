namespace WeightAndSee.Models;
public partial class BarbellModel : BaseModel
{  
    public BarbellModel(IWeightConversionService weightConversionService) : base(weightConversionService)
    {
        BarLine.X2 = 200;
        // Don't call CreateItemForDisplay here - it will be called when needed
        // Subscribe to collection changes
        //LeftPlates.CollectionChanged += Plates_CollectionChanged;
        //RightPlates.CollectionChanged += Plates_CollectionChanged;
    }

}