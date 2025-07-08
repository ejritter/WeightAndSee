namespace WeightAndSee.Models;
public partial class BarbellModel : BaseModel
{  
    public BarbellModel(IWeightConversionService weightConversionService) : base(weightConversionService)
    {
        BarLine.X2 = 200;
    }

}