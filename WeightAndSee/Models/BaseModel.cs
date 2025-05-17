using System.Collections.ObjectModel;

namespace WeightAndSee.Models;

public abstract partial class BaseModel : ObservableObject , IWeightConversionService
{
    [ObservableProperty]
    private double _barWeight;

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _leftPlates = new();

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _rightPlates = new();

    [ObservableProperty]
    private string _barType = string.Empty;

    public double TotalWeight => KilogramToPound(this);

    private const double kgUnit = 2.20462262815;

    public double KilogramToPound(BaseModel bar)
    {
        //bug here where the bar is getting silly amounts of plates on each side.
        double output = 0;
        output = bar.BarWeight * kgUnit;

        foreach (KiloPlateModel leftPlate in bar.LeftPlates)
        {
            output += leftPlate.KiloGram * kgUnit;
        }

        foreach (KiloPlateModel rightPlate in bar.RightPlates)
        {
            output += rightPlate.KiloGram * kgUnit;
        }

        return output;
    }


    public void AddPlatesToThisBar(BaseModel bar, double weight, ObservableCollection<KiloPlateModel> availableKiloPlates )
    {
        var plateIndex = 0;
        do
        {
            var kiloPlate = availableKiloPlates[plateIndex];//starts at 25kg and goes down.
            var leftPlate = new KiloPlateModel { KiloPlate = kiloPlate.KiloPlate, KiloGram = kiloPlate.KiloGram };
            var rightPlate = new KiloPlateModel { KiloPlate = kiloPlate.KiloPlate, KiloGram = kiloPlate.KiloGram };
            var totalPlateKG = (leftPlate.KiloGram + rightPlate.KiloGram) * kgUnit;
            if (bar.TotalWeight + totalPlateKG <= weight)
            {
                bar.LeftPlates.Add(leftPlate);
                bar.RightPlates.Add(rightPlate);
            }
            else
            {
                //go to the next set of plates.
                plateIndex += 1;
            }
            //we've exceeded our available plates.
            if (plateIndex == availableKiloPlates.Count)
                break;


        } while (bar.TotalWeight <= weight);

        //do
        //{
        //    for(int i = 0; i < availableKiloPlates.Count; i ++)
        //    //foreach (KiloPlateModel kiloPlate in availableKiloPlates)
        //    {
        //        var kiloPlate = availableKiloPlates[i];
        //        var leftPlate = new KiloPlateModel { KiloPlate = kiloPlate.KiloPlate, KiloGram = kiloPlate.KiloGram };
        //        var rightPlate = new KiloPlateModel { KiloPlate = kiloPlate.KiloPlate, KiloGram = kiloPlate.KiloGram };
        //        var results = (leftPlate.KiloGram + rightPlate.KiloGram) * kgUnit;
        //        //I think here we want to do:
        //        //bar.TotalWeight + results <= weight. Then add the plates.
        //        //If it's not go to the next set of plates and repeat.
        //        //do this for all plates and when we reach then end, we know we've maxed out.
        //        if(bar.TotalWeight + results <= weight)
        //        {
        //            bar.LeftPlates.Add(leftPlate);
        //            bar.RightPlates.Add(rightPlate);
        //            break;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
            
        //} while (bar.TotalWeight <= weight);
    }

}
