
namespace WeightAndSee.Services;

public class WeightConversionService : IWeightConversionService
{

    public int KilogramToPound(BaseModel bar)
    {
        double platesInKg = 0;
        foreach (KiloPlateModel leftPlate in bar.LeftPlates)
        {
            platesInKg += leftPlate.KiloGram;
        }
        foreach (KiloPlateModel rightPlate in bar.RightPlates)
        {
            platesInKg += rightPlate.KiloGram;
        }

        // BarWeight is already in pounds.
        // Convert total plate weight from KG to pounds and add to bar weight.
        return (int)(bar.BarWeight + (platesInKg * Constants.kgUnitToPounds));
    }


    public double PoundToKilogram(BaseModel bar)
    {
        // Convert bar weight (which is in pounds) to kilograms.
        double totalKilograms = bar.BarWeight * Constants.poundsToKgUnit;

        // Add weight of plates (which are already in kilograms).
        foreach (KiloPlateModel leftPlate in bar.LeftPlates)
        {
            totalKilograms += leftPlate.KiloGram;
        }
        foreach (KiloPlateModel rightPlate in bar.RightPlates)
        {
            totalKilograms += rightPlate.KiloGram;
        }
        return totalKilograms;
    }
}
