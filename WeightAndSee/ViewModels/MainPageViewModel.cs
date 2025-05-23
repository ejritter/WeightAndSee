

namespace WeightAndSee.ViewModels;
public partial class MainPageViewModel : BaseViewModel
{


    public MainPageViewModel(IPopupService popupService) : base(popupService)
    {
        LoadAllPlates();
    }

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _kiloPlateModels = new();

    [ObservableProperty]
    private BaseModel _barType = null;

    [ObservableProperty]
    private ContentView _barView = null;

    [ObservableProperty]
    private string _barWeightText = string.Empty;

    [ObservableProperty]
    private string _desiredWeightText = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowReport))]
    [NotifyPropertyChangedFor(nameof(ShowBar))]
    private string _barReport = string.Empty;

    public bool ShowReport => !string.IsNullOrEmpty(BarReport);

    public bool ShowBar => !string.IsNullOrEmpty(BarReport);

    private void LoadAllPlates()
    {
        KiloPlateModels.Clear();
        var kiloPlateModels = Enum.GetValues(typeof(KiloPlates))
                                   .Cast<KiloPlates>()
                                   .Select(plate =>
                                   {
                                       var kpModel = new KiloPlateModel()
                                       {
                                           KiloPlate = plate.ToString(),
                                           KiloGram = plate.GetWeightInKg(),
                                           PlateColor = plate.GetWeightColor(),
                                           PlateSize = plate.GetPlateSize()
                                       };
                                       return kpModel;
                                   })
                                   .OrderByDescending(kp => kp.KiloGram )
                                   .ToList();

        foreach (KiloPlateModel plateModel in kiloPlateModels)
        {
            KiloPlateModels.Add(plateModel);
        }
    }


    [RelayCommand]
    private async void AddBar()
    {
        BarReport = string.Empty;
        if (BarWeightText == string.Empty)
        {
            var results = await ShowPopupAsync("Warning!", "Bar Weight cannot be blank", false);
            return;
        }
        if (double.TryParse(BarWeightText, out _) == false)
        {
            var results = await ShowPopupAsync("Warning!", "Bar Weight must be a valid number", false);
            return;
        }
        if (DesiredWeightText == string.Empty)
        {
            var results = await ShowPopupAsync("Warning!", "Desired Weight cannot be blank", false);
            return;
        }
        if (double.TryParse(DesiredWeightText, out _) == false)
        {
            var results = await ShowPopupAsync("Warning!", "Desired Weight must be a valid number", false);
            return;
        }
        if (BarType is null)
        {
            var results = await ShowPopupAsync("Warning!", "Bar Type must be selected", false);
            return;
        }

        BarType.ResetBar();
        BarType.SetBarType();
        BarType.SetBarWeight(double.Parse(BarWeightText));
        BarType.AddPlatesToBar(double.Parse(DesiredWeightText), KiloPlateModels);
        BarReport = BarType.BarReport();
        BarView = BarType.DisplayItem();

    }


    [RelayCommand]
    private void PickerSet(object? sender)
    {
        if (sender is Picker picker &&
                picker.SelectedItem is BaseModel bt)
        {
            BarType = bt;
        }
        else
        {
            BarType = null;
        }
    }
}
