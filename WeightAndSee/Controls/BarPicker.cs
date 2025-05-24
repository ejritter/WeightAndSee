namespace WeightAndSee.Controls;
public class BarPicker : ContentView
{
    public BarPicker()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        var picker = new Picker()
        {
            ItemsSource = new List<BaseModel>
                            {
                             new BarbellModel(){BarType = BarTypes.Barbell.ToString()},
                             new DumbbellModel(){BarType = BarTypes.Dumbbell.ToString()}
                            },
            ItemDisplayBinding = new Binding(nameof(BaseModel.BarType)),
            Title = "Bar Type"
        }
    .Column(Column.PickerEntry)
    .Row(Row.BarInfo)
    .Fill()
    .Start()
    .Bind(Picker.SelectedItemProperty, getter: (MainPageViewModel vm) => vm.BarType,
                                       setter: (MainPageViewModel vm, BaseModel bt) => vm.BarType = bt);

        picker.Behaviors.Add(new EventToCommandBehavior
        {
            EventName = "SelectedIndexChanged",
            Command = new Command(() =>
            {
                if (this.BindingContext is MainPageViewModel vm)
                {
                    vm.PickerSetCommand.Execute(picker);
                }
            })
        });

        Content = new ScrollView()
        {
            Content = new Grid()
            {
                ColumnSpacing = 10,
                Padding = 10,
                Margin = 10,
                RowDefinitions = Rows.Define(
                    (Row.BarInfo, 80),
                    (Row.DesiredWeight, 80)),

                ColumnDefinitions = Columns.Define(
                    (Column.PickerEntry, 80),
                    (Column.BarWeightButton, 80)),

                Children =
                {


                    picker
                        .Column(Column.PickerEntry)
                        .Row(Row.BarInfo),

                    new Entry(){ Keyboard = Keyboard.Numeric }
                        .Placeholder("Bar weight")
                        .Column(Column.BarWeightButton)
                        .Row(Row.BarInfo)
                        .Fill()
                        .Start()
                         
                        .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                                  getter:(MainPageViewModel vm) => vm.BarWeightText,
                                                  setter:(MainPageViewModel vm, string? barWeightText) => vm.BarWeightText = barWeightText ?? string.Empty),

                    new Entry(){ Keyboard = Keyboard.Numeric }
                        .Placeholder("Desired weight")
                        .Column(Column.PickerEntry)
                        .Row(Row.DesiredWeight)
                        .Fill()
                        .Start()
                        .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                                  getter:(MainPageViewModel vm) => vm.DesiredWeightText,
                                                  setter:(MainPageViewModel vm, string? desiredWeightText) => vm.DesiredWeightText = desiredWeightText ?? string.Empty),

                    new ImageButton()
                    {
                        Source = "ic_fluent_add_48_filled.svg"
                    }
                    .Column(Column.BarWeightButton)
                    .Row(Row.DesiredWeight)
                    .Fill()
                    .Start()
                    .Bind(ImageButton.CommandProperty, getter:(MainPageViewModel vm) => vm.AddBarCommand)

                }

            }.Top().Center()
        };
    }

    private enum Row { BarInfo, DesiredWeight }
    private enum Column { PickerEntry, BarWeightButton }
}
