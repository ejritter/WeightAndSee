namespace WeightAndSee.Services;
public static class Extensions
{

    public static double GetWeightInKg(this KiloPlates plate)
    {
        return plate switch
        {
            KiloPlates.Kg_0_25 => 0.25,
            KiloPlates.Kg_0_5 => 0.5,
            KiloPlates.Kg_1_0 => 1.0,
            KiloPlates.Kg_1_25 => 1.25,
            KiloPlates.Kg_1_5 => 1.5,
            KiloPlates.Kg_2_0 => 2.0,
            KiloPlates.Kg_2_5 => 2.5,
            KiloPlates.Kg_5_0 => 5.0,
            KiloPlates.Kg_10_0 => 10.0,
            KiloPlates.Kg_15_0 => 15.0,
            KiloPlates.Kg_20_0 => 20.0,
            KiloPlates.Kg_25_0 => 25.0,
            _ => throw new ArgumentOutOfRangeException(nameof(plate), $"Unsupported plate type: {plate}")

        };
    }
    public static void SetBarWeight(this BaseModel bar, double weight)
    {
        bar.BarWeight = weight;
    }
    public static void SetBarType(this BaseModel bar)
    {
       if (bar is BarbellModel)
        {
            bar.BarType = BarTypes.Barbell.ToString();
        }
       if (bar is DumbbellModel)
        {
            bar.BarType = BarTypes.Dumbbell.ToString();
        }
    }

    public static void AddPlatesToBar(this BaseModel bar, double weight, ObservableCollection<KiloPlateModel> availableKiloPlates)
    {
        bar.AddPlatesToThisBar(bar,weight,availableKiloPlates);
    }

}
