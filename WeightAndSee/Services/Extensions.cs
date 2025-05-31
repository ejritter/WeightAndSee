namespace WeightAndSee.Services;
public static class Extensions
{
    private static double kgUnitToKilograms = 0.45359237;
    private static double kgUnitToPound = 2.20462262815;
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
            return 50;
        }
       else
        {
            return 30;
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
            var totalPlateWeightInPounds = (currentPlate.KiloGram * 2) * kgUnitToPound;
            
            if (((int)totalPlateWeightInPounds + bar.TotalWeightInPounds) <= weight &&
                    currentPlate.IsAvailable)
            {
                bar.LeftPlates.Add(new KiloPlateModel()
                {
                    KiloGram = currentPlate.KiloGram,
                    KiloPlate = currentPlate.KiloPlate,
                    PlateColor = currentPlate.PlateColor,
                    PlateSize = currentPlate.PlateSize
                });

                bar.RightPlates.Add(new KiloPlateModel()
                {
                    KiloGram = currentPlate.KiloGram,
                    KiloPlate = currentPlate.KiloPlate,
                    PlateColor = currentPlate.PlateColor,
                    PlateSize = currentPlate.PlateSize
                });
            }
            else if(plateIndex != availableKiloplates.Count -1)
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
        bar.ResetBarDisplay();
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

    public static VerticalStackLayout BarReportView(this BaseModel bar)
    {
        VerticalStackLayout output = new()
        {
            Margin = 3,
            Spacing = 3,
            Padding = 3
        };
        var plateCount = 0;
        var uniquePlates = bar.LeftPlates
                                .Concat(bar.RightPlates)
                                .DistinctBy(p => p.KiloPlate);
        //return vertical stack layout labels.
        //need one label for the Your Bar Needs:
        //then each new label from here should change the text color of the KiloGram Plate.
        //we need to make a horizontalGroup for the labels so something like:
        //<verticalGroup>
        //one for each label => <horizontalGroup>
        //</verticalGroup>

        List<HorizontalStackLayout> plateCountLabels = new();
        plateCountLabels.Add(new HorizontalStackLayout()
        {
               Padding = 3,
                Margin = 3,
                Spacing = 3,
                Children =
                {
                    new Label()
                        .FontSize(22)
                        .TextColor(Colors.LightSlateGray)
                        .Text($"Your bar needs:")
                }
        });

        foreach (KiloPlateModel plate in uniquePlates)
        {
            plateCount += bar.LeftPlates.Where(lp => lp.KiloPlate == plate.KiloPlate)
                            .Count();
            plateCount += bar.RightPlates.Where(rp => rp.KiloPlate == plate.KiloPlate)
                             .Count();
            var newHorizontalGroup = new HorizontalStackLayout()
            {
                Padding = 3,
                Margin = 3,
                Spacing = 3,
                Children =
                {
                    new Label()
                    {
                        FontSize = 16,
                        FormattedText = new FormattedString
                        {
                            Spans =
                            {
                                new Span { Text = $"\t{plateCount}", TextColor = plate.PlateColor},
                                new Span { Text = " of " , TextColor = Colors.LightSlateGray},
                                new Span { Text = $"{plate.KiloGram}.", TextColor = plate.PlateColor}
                            }
                        }
                    }
                }
            };
            plateCountLabels.Add(newHorizontalGroup);
            plateCount = 0;//reset for next plate.
        }
        plateCountLabels.Add(new HorizontalStackLayout()
        {
               Padding = 3,
                Margin = 3,
                Spacing = 3,
                Children =
                {
                    new Label()
                        .FontSize(22)
                        .TextColor(Colors.LightSlateGray)
                        .Text($"For a total weight of {bar.TotalWeightInPounds } pounds.")
                }
        });
        foreach (HorizontalStackLayout stack in plateCountLabels)
        {

            output.Add(stack);
        }
        return output;
    }
}
