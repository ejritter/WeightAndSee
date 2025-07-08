namespace WeightAndSee.Models;
public partial class DumbbellModel : BaseModel
{
    public DumbbellModel(IWeightConversionService weightConversionService) : base(weightConversionService)
    {
        BarLine.X2 = 30;
    }
   
}
