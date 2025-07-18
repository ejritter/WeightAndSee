namespace WeightAndSee.Extensions;

public static class KiloPlateModelExtensions
{
    public static KiloPlateModel ClonePlate(this KiloPlateModel kp)
    {
        var output = new KiloPlateModel(kp.WeightConversionService)
        {
            KiloGram = kp.KiloGram,
            KiloPlate = kp.KiloPlate,
            PlateColor = kp.PlateColor,
            PlateSize = kp.PlateSize,
            IsAvailable = kp.IsAvailable
        };

        return output;
    }
}
