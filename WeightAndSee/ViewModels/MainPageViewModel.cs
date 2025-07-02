namespace WeightAndSee.ViewModels;
public partial class MainPageViewModel : BaseViewModel
{
    public MainPageViewModel(IPopupService popupService, IWeightConversionService weightConversionService,
            IViewCreatorService viewCreatorService) : base(popupService, weightConversionService, viewCreatorService)
    {
        var barBell = new BarbellModel(base.WeightConversionService) { BarType = BarTypes.Barbell.ToString() };
            barBell.LeftPlates.CollectionChanged += ViewCreatorService.Plates_CollectionChanged;
            barBell.RightPlates.CollectionChanged += ViewCreatorService.Plates_CollectionChanged;

        var dumbBell = new DumbbellModel(base.WeightConversionService) { BarType = BarTypes.Dumbbell.ToString() };
            dumbBell.LeftPlates.CollectionChanged += ViewCreatorService.Plates_CollectionChanged;
            dumbBell.RightPlates.CollectionChanged += ViewCreatorService.Plates_CollectionChanged;
        
        BarTypesList.Add(barBell);
        BarTypesList.Add(dumbBell);
        LoadAllPlates();
    }

    [ObservableProperty]
    private ObservableCollection<BaseModel> _barTypesList = new();

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

    private bool _canSubmit = false;

    [ObservableProperty]
    private VerticalStackLayout _barReportView = new VerticalStackLayout();
    public bool ShowReport => !string.IsNullOrEmpty(BarReport);

    public bool ShowBar => !string.IsNullOrEmpty(BarReport);

    private void LoadAllPlates()
    {
        KiloPlateModels.Clear();
        PlatesAvailableToYou.Clear();
        var kiloPlateModels = Enum.GetValues(typeof(KiloPlates))
                                   .Cast<KiloPlates>()
                                   .Select(plate =>
                                   {
                                       var kpModel = new KiloPlateModel(new WeightConversionService())
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


  

    [RelayCommand]
    private async void AddBar()
    {
        BarReport = string.Empty;
        if (BarWeightText == string.Empty)
        {
            _ = await ShowPopupAsync("Warning!", "Bar Weight cannot be blank", false);
            return;
        }
        if (double.TryParse(BarWeightText, out _) == false)
        {
            _ = await ShowPopupAsync("Warning!", "Bar Weight must be a valid number", false);
            return;
        }
        if (DesiredWeightText == string.Empty)
        {
            _ = await ShowPopupAsync("Warning!", "Desired Weight cannot be blank", false);
            return;
        }
        if (double.TryParse(DesiredWeightText, out _) == false)
        {
            _ = await ShowPopupAsync("Warning!", "Desired Weight must be a valid number", false);
            return;
        }
        if (BarType is null)
        {
            _ = await ShowPopupAsync("Warning!", "Bar Type must be selected", false);
            return;
        }

        BarType.ResetBar();
        BarType.SetBarType();
        BarType.SetBarWeight(double.Parse(BarWeightText));
        BarType.AddPlatesToBar(double.Parse(DesiredWeightText), PlatesAvailableToYou);
        BarReport = BarType.BarReport();
        BarReportView = BarType.BarReportView();
        ViewCreatorService.SetViewBar(BarType);
        //BarView = BarType.DisplayItem;
        BarView = ViewCreatorService.DisplayView;
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
