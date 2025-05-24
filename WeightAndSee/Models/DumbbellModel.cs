namespace WeightAndSee.Models;
public partial class DumbbellModel : BaseModel
{
    public DumbbellModel()
    {
        _barLine.X2 = 30;
        LeftPlates.CollectionChanged += Plates_CollectionChanged;
        RightPlates.CollectionChanged += Plates_CollectionChanged;
    }

}
