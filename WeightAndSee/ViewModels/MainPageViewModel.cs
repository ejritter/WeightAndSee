

using CommunityToolkit.Maui.Core.Platform;

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
    private ObservableCollection<object> _selectedPlates = new();

    [ObservableProperty]
    private BaseModel _barType = null;

    [ObservableProperty]
    private ContentView _barView = null;

    [ObservableProperty]
    private string _barWeightText = string.Empty;

    [ObservableProperty]
    private string _desiredWeightText = string.Empty;

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _platesAvailableToYou = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowReport))]
    [NotifyPropertyChangedFor(nameof(ShowBar))]
    private string _barReport = string.Empty;

    [ObservableProperty]
    private bool _canSubmit = false;

    [ObservableProperty]
    private VerticalStackLayout _barReportView = new VerticalStackLayout();
    public bool ShowReport => !string.IsNullOrEmpty(BarReport);

    public bool ShowBar => !string.IsNullOrEmpty(BarReport);

    private CancellationTokenSource? _debounceCts;
    private const int DebounceDelayMilliseconds = 1500; // Adjust delay as needed

    private void LoadAllPlates()
    {
        KiloPlateModels.Clear();
        PlatesAvailableToYou.Clear();
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
                                   .OrderByDescending(kp => kp.KiloGram)
                                   .ToList();

        foreach (KiloPlateModel plateModel in kiloPlateModels)
        {
            KiloPlateModels.Add(plateModel);
            PlatesAvailableToYou.Add(plateModel.ClonePlate());
        }
    }

    // This method contains the core logic to update CanSubmit
    private void PerformCanSubmitCheck()
    {
        CanSubmit = !string.IsNullOrEmpty(BarWeightText) && !string.IsNullOrEmpty(DesiredWeightText);
    }

    // New command to be called by EventToCommandBehavior from TextChanged event
    [RelayCommand]
    private async Task HandleInputChanged(object? sender)
    {
        _debounceCts?.Cancel(); // Cancel the previous debounce task
        _debounceCts?.Dispose();
        _debounceCts = new CancellationTokenSource();

        try
        {
            await Task.Delay(DebounceDelayMilliseconds, _debounceCts.Token);
            // After the delay, perform the actual check
            if (sender is Entry entry)
            {
                await entry.HideKeyboardAsync();
            }
            PerformCanSubmitCheck();
        }
        catch (TaskCanceledException)
        {
            // This is expected if typing continues and cancels the delay
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
        BarType.AddPlatesToBar(double.Parse(DesiredWeightText), PlatesAvailableToYou);
        BarReport = BarType.BarReport();
        BarReportView = BarType.BarReportView();
        BarView = BarType.DisplayItem;
        new Entry().HideKeyboardAsync();
    }

    [RelayCommand]
    private void PickerSet(object? sender)
    {
        if (sender is Picker picker &&
                picker.SelectedItem is BaseModel bt)
        {
            BarType = bt;
            DesiredWeightText = string.Empty;
            BarWeightText = string.Empty;
        }
        else
        {
            BarType = null;
        }
        PerformCanSubmitCheck();
    }

    [RelayCommand]
    private void SetPlateToAvailable(object? sender)
    {
        if (sender is CollectionView cv && cv.SelectedItem is KiloPlateModel kp)
        {
            cv.SelectedItem = null;
            var found = PlatesAvailableToYou.FirstOrDefault(pa => pa.KiloGram == kp.KiloGram);
            if (found is not null)
            {
                _ = ShowPopupAsync(title: "Heads up!", message: $"plate {kp.KiloGram} already available.", isDismissable: true);
                return;
            }
            else
            {
                //if the plate is 25 make it first or 0.25 make it last
                if (kp.KiloGram == 25)
                {
                    PlatesAvailableToYou.Insert(0, kp.ClonePlate());
                }
                else if (kp.KiloGram == 0.25)
                {
                    PlatesAvailableToYou.Insert(PlatesAvailableToYou.Count, kp.ClonePlate());
                }
                else
                {
                    var currentPlates = PlatesAvailableToYou.ToList();
                    foreach (KiloPlateModel currentPlate in currentPlates)
                    {
                        if (kp.KiloGram > currentPlate.KiloGram)
                        {
                            PlatesAvailableToYou.Insert(PlatesAvailableToYou.IndexOf(currentPlate), kp.ClonePlate());
                            break;
                        }
                    }
                }
                _ = ShowPopupAsync(title: "Heads up!", message: $"Plate {kp.KiloGram} added.", isDismissable: true);
            }
        }
    }

    [RelayCommand]
    private void SetPlateToUnavailable(object? sender)
    {
        if (sender is CollectionView cv && cv.SelectedItem is KiloPlateModel kp)
        {
            cv.SelectedItem = null;
            var found = PlatesAvailableToYou.FirstOrDefault(pa => pa.KiloGram == kp.KiloGram);
            if (found is not null)
            {
                PlatesAvailableToYou.Remove(found);
                _ = ShowPopupAsync(title: "Heads up!", message: $"Plate {kp.KiloGram} removed.", isDismissable: true);
            }
            else
            {
                return;
            }
        }
    }
}
