﻿namespace WeightAndSee.ViewModels;
public partial class MainPageViewModel : BaseViewModel
{
    public MainPageViewModel(IPopupService popupService, IWeightConversionService weightConversionService,
            IViewCreatorService viewCreatorService, IToastService toastService) : base(popupService, weightConversionService, viewCreatorService, toastService)
    {
        _barbell = new BarbellModel(base.WeightConversionService) { BarType = BarTypes.Barbell.ToString() };
            _barbell.LeftPlates.CollectionChanged += ViewCreatorService.Plates_CollectionChanged;
            _barbell.RightPlates.CollectionChanged += ViewCreatorService.Plates_CollectionChanged;

        _dumbbell = new DumbbellModel(base.WeightConversionService) { BarType = BarTypes.Dumbbell.ToString() };
            _dumbbell.LeftPlates.CollectionChanged += ViewCreatorService.Plates_CollectionChanged;
            _dumbbell.RightPlates.CollectionChanged += ViewCreatorService.Plates_CollectionChanged;
       

        ImageButtonIcon = Application.Current.RequestedTheme == AppTheme.Dark ?
            "add_plate_dark_icon.png" : "add_plate_light_icon.png";
        LoadAllPlates();
    }

    [ObservableProperty]
    private string _imageButtonIcon;

    [ObservableProperty]
    private double _maximumPlateHeight;

    [ObservableProperty]
    private bool _isBarbellChecked = false;

    [ObservableProperty]
    private bool _isDumbbellChecked = false;

    [ObservableProperty]
    private BarbellModel _barbell = null;

    [ObservableProperty]
    private DumbbellModel _dumbbell = null;

    [ObservableProperty]
    private ObservableCollection<BaseModel> _barTypesList = new();

    [ObservableProperty]
    private ObservableCollection<KiloPlateModel> _kiloPlateModels = new();

    [ObservableProperty]
    private ObservableCollection<object> _selectedPlates = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowReport))]
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
    private ContentView _barReportView = new ContentView();

    [ObservableProperty]
    public bool _showReport = false;

    public bool ShowBar => ShowReport;

    private void LoadAllPlates()
    {
        KiloPlateModels.Clear();
        PlatesAvailableToYou.Clear();
        var kiloPlateModels = Enum.GetValues(typeof(KiloPlates))
                                   .Cast<KiloPlates>()
                                   .Select(plate =>
                                   {
                                       var kpModel = new KiloPlateModel(WeightConversionService)
                                       {
                                           KiloPlate = plate.ToString(),
                                           KiloGram = plate.GetWeightInKg(),
                                           PlateColor = plate.GetWeightColor(),
                                           PlateSize = plate.GetPlateSize()
                                       };
                                       kpModel.IsAvailable = Preferences.Get(key: kpModel.KiloPlate, defaultValue: true);
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
        ShowReport = false;
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

        //bar setup
        ViewCreatorService.ResetBarDisplay();
        BarType.ResetBar();
        BarType.SetBarType();
        BarType.SetBarWeight(double.Parse(BarWeightText));
        BarType.AddPlatesToBar(double.Parse(DesiredWeightText), PlatesAvailableToYou);
        ViewCreatorService.SetViewBar(BarType);

        //display 
        BarReportView = ViewCreatorService.BarReportView(BarType);
        MaximumPlateHeight = BarType.LeftPlates.Max(p => p.PlateSize) + 10; // Add some padding
        BarView = ViewCreatorService.DisplayItem;
        new Entry().HideKeyboardAsync();
        ShowReport = true;
    }


    [RelayCommand]
    private void SetBarType(object? sender)
    {
       
        if (sender is BarTypes barType)
        {
            if (barType == BarTypes.Barbell)
            {
                BarType = Barbell;
            }
            else if (barType == BarTypes.Dumbbell)
            {
                BarType = Dumbbell;
            }
        }
    }

    [RelayCommand]
    private async Task SetPlateStatus(object? sender)
    {
        if (sender is KiloPlateModel pm)
        {
            var toastMessage = "";
            pm.IsAvailable = !pm.IsAvailable;
            Preferences.Set(key: pm.KiloPlate, value: pm.IsAvailable);
            toastMessage = pm.IsAvailable ? "Plate Available" : "Plate Unavailable";
            await ToastService.ShowToastAsync(message: toastMessage);
        }
    }

    
}
