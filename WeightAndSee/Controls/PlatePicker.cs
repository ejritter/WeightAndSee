namespace WeightAndSee.Controls;

public partial class PlatePicker : ContentView
{
    public static readonly BindableProperty PlatesProperty =
        BindableProperty.Create(
            nameof(Plates),
            typeof(IEnumerable<KiloPlateModel>),
            typeof(PlatePicker),
            defaultValue: null,
            propertyChanged: OnPlatesChanged);

    public IEnumerable<KiloPlateModel> Plates
    {
        get => (IEnumerable<KiloPlateModel>)GetValue(PlatesProperty);
        set => SetValue(PlatesProperty, value);
    }

    private static void OnPlatesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PlatePicker picker && picker.PlateStoreCollectionView != null)
        {
            picker.PlateStoreCollectionView.ItemsSource = newValue as IEnumerable<KiloPlateModel>;
        }
    }

    public PlatePicker()
    {
        InitializeComponent();
    }
}
