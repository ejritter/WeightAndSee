namespace WeightAndSee.Extensions;

public static class KiloPlatesExtensions
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

     public static double GetPlateSize(this KiloPlates plate)
    {
        if (plate.GetWeightInKg() > 9)
        {
            return Constants.largePlate;
        }
        else
        {
            return Constants.smallPlate;
        }
    }
    public static Color GetWeightColor(this KiloPlates plate)
    {
        return plate switch
        {
            KiloPlates.Kg_0_25 => Colors.Silver,
            KiloPlates.Kg_0_5 => Colors.Silver,
            KiloPlates.Kg_1_0 => Colors.Silver,
            KiloPlates.Kg_1_25 => Colors.Silver,
            KiloPlates.Kg_1_5 => Colors.Silver,
            KiloPlates.Kg_2_0 => Colors.Silver,
            KiloPlates.Kg_2_5 => Colors.Black,
            KiloPlates.Kg_5_0 => Colors.White,
            KiloPlates.Kg_10_0 => Colors.Green,
            KiloPlates.Kg_15_0 => Colors.Yellow,
            KiloPlates.Kg_20_0 => Colors.Blue,
            KiloPlates.Kg_25_0 => Colors.Red,

            _ => throw new ArgumentOutOfRangeException(nameof(plate), $"Unsupported plate type: {plate}")
        };
    }
}
