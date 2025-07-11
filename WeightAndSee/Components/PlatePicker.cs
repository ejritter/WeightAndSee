using System.Windows.Input;

namespace WeightAndSee.Components;

public partial class PlatePicker : ContentView
{

    public PlatePicker()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty MaximumPlateHeightProperty =
                BindableProperty.Create(
                    propertyName: nameof(MaximumPlateHeight),
                    returnType: typeof(double),
                    declaringType: typeof(PlatePicker)
                    );
                
public double MaximumPlateHeight
    {
        get => (double)GetValue(MaximumPlateHeightProperty);
        set => SetValue(MaximumPlateHeightProperty, value);
    }

    public static readonly BindableProperty SetPlateStatusCommandProperty =
        BindableProperty.Create(
                nameof(SetPlateStatusCommand), 
                typeof(ICommand), 
                typeof(PlatePicker), 
                null);

    public ICommand SetPlateStatusCommand
    {
        get => (ICommand)GetValue(SetPlateStatusCommandProperty);
        set => SetValue(SetPlateStatusCommandProperty, value);
    }


    public static readonly BindableProperty PlatesProperty =
        BindableProperty.Create(
            nameof(Plates),
            typeof(IEnumerable<KiloPlateModel>),
            typeof(PlatePicker),
            defaultValue: null);

    public IEnumerable<KiloPlateModel> Plates
    {
        get => (IEnumerable<KiloPlateModel>)GetValue(PlatesProperty);
        set => SetValue(PlatesProperty, value);
    }
}
