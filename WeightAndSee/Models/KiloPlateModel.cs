namespace WeightAndSee.Models;
public partial class KiloPlateModel: BaseModel
{
    [ObservableProperty]
    private double _kiloGram;

    [ObservableProperty]
    private string _KiloPlate;
}
