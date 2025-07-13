namespace WeightAndSee.Extensions;

public static class BaseModelExtensions
{

    public static Line CloneBarLine(this BaseModel bar)
    {
        //when drawing lines, always work with a new one.
        //trying to reuse a line causes havok
        return new Line
        {
            X1 = bar.BarLine.X1,
            X2 = bar.BarLine.X2,
            Y1 = bar.BarLine.Y1,
            Y2 = bar.BarLine.Y2,
            Stroke = bar.BarLine.Stroke,
            StrokeThickness = bar.BarLine.StrokeThickness,
            HorizontalOptions = bar.BarLine.HorizontalOptions,
            VerticalOptions = bar.BarLine.VerticalOptions,
            TranslationY = bar.BarLine.TranslationY
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

    public static void AddPlatesToBar(this BaseModel bar, double weight, ObservableCollection<KiloPlateModel> availableKiloplates)
    {
        var plateIndex = 0;
        do
        {
            var currentPlate = availableKiloplates[plateIndex];
            var totalPlateWeightInPounds = (currentPlate.KiloGram * 2) * Constants.kgUnitToPounds;

            if (((int)totalPlateWeightInPounds + bar.TotalWeightInPounds) <= weight &&
                    currentPlate.IsAvailable)
            {
                bar.LeftPlates.Add(new KiloPlateModel(currentPlate.WeightConversionService)
                {
                    KiloGram = currentPlate.KiloGram,
                    KiloPlate = currentPlate.KiloPlate,
                    PlateColor = currentPlate.PlateColor,
                    PlateSize = currentPlate.PlateSize
                });

                bar.RightPlates.Add(new KiloPlateModel(currentPlate.WeightConversionService)
                {
                    KiloGram = currentPlate.KiloGram,
                    KiloPlate = currentPlate.KiloPlate,
                    PlateColor = currentPlate.PlateColor,
                    PlateSize = currentPlate.PlateSize
                });
            }
            else if (plateIndex != availableKiloplates.Count - 1)
            {
                plateIndex += 1;
            }
            else
            {
                //no more plates
                break;
            }

        } while (bar.TotalWeightInPounds <= weight);
    }

    public static void ResetBar(this BaseModel bar)
    {
        bar.BarWeight = 0;
       // bar.ResetBarDisplay();
        bar.LeftPlates.Clear();
        bar.RightPlates.Clear();
    }


    public static string BarReport(this BaseModel bar)
    {
        var output = new StringBuilder();
        var plateCount = 0;
        var uniquePlates = bar.LeftPlates
                            .Concat(bar.RightPlates)
                            .DistinctBy(p => p.KiloPlate);
        output.AppendLine("Your bar needs:");
        foreach (KiloPlateModel plate in uniquePlates)
        {
            plateCount += bar.LeftPlates.Where(lp => lp.KiloPlate == plate.KiloPlate)
                                        .Count();

            plateCount += bar.RightPlates.Where(rp => rp.KiloPlate == plate.KiloPlate)
                                         .Count();

            output.AppendLine($"\t{plateCount} of {plate.KiloGram} plates");
            plateCount = 0;
        }
        output.AppendLine();
        output.AppendLine($"For a total weight of {bar.TotalWeightInPounds} pounds.");
        return output.ToString();
    }
}
