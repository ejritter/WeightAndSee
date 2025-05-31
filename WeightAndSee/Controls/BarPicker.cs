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
            TitleColor = Colors.DarkGrey,
            TextColor = Colors.LightSlateGray,
            Title = "Bar Type:",
            FontSize = 22,
            Background = Color.FromArgb("#FAFAFA")
        };

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


        var barWeightEntry = new Entry() 
        { 
            Keyboard = Keyboard.Numeric, 
            FontSize = 22, 
            PlaceholderColor = Colors.LightSlateGray,
            TextColor = Colors.LightSlateGray 
        };
        barWeightEntry.Behaviors.Add(new EventToCommandBehavior
        {
            EventName = nameof(Entry.TextChanged),
            Command = new Command(() =>
            {
                if (this.BindingContext is MainPageViewModel vm)
                {
                    // Call the new debounced command
                    vm.HandleInputChangedCommand.Execute(barWeightEntry);
                }
            })
        });

        var desiredWeightEntry = new Entry() 
        { 
          Keyboard = Keyboard.Numeric, 
          FontSize = 22, 
          PlaceholderColor = Colors.LightSlateGray,
          TextColor = Colors.LightSlateGray 
        };
        desiredWeightEntry.Behaviors.Add(new EventToCommandBehavior
        {
            EventName = nameof(Entry.TextChanged),
            Command = new Command(() =>
            {
                if (this.BindingContext is MainPageViewModel vm)
                {
                    // Call the new debounced command
                    vm.HandleInputChangedCommand.Execute(desiredWeightEntry);
                }
            })

        });
        Content = new ScrollView()
        {
            Content = new Grid()
            {
                RowSpacing = 5,
                Padding = 10,
                Margin = 10,
                RowDefinitions = Rows.Define(
                    (Row.BarType, 80),
                    (Row.BarWeight, 80),
                    (Row.DesiredWeight, 80),
                    (Row.AddPlateButton, 250)),

                ColumnDefinitions = Columns.Define(
                    (Column.UserControls, Star)),

                Children =
                {

                    picker    
                    .Column(Column.UserControls)
                    .Row(Row.BarType)
                    .Fill()
                    .Bind(Picker.SelectedItemProperty, getter: (MainPageViewModel vm) => vm.BarType,
                                                       setter: (MainPageViewModel vm, BaseModel bt) => vm.BarType = bt),
                    barWeightEntry
                        .Placeholder(text:"Bar weight in pounds")
                        .TextColor(Colors.LightSlateGray)
                        .Column(Column.UserControls)
                        .Row(Row.BarWeight)
                        .Fill()                         
                        .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                                  getter:(MainPageViewModel vm) => vm.BarWeightText,
                                                  setter:(MainPageViewModel vm, string? barWeightText) => vm.BarWeightText = barWeightText ?? string.Empty),

                    desiredWeightEntry
                        .Placeholder(text:"Desired weight in pounds")
                        .Column(Column.UserControls)
                        .Row(Row.DesiredWeight)
                        .Fill()
                        .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                                  getter:(MainPageViewModel vm) => vm.DesiredWeightText,
                                                  setter:(MainPageViewModel vm, string? desiredWeightText) => vm.DesiredWeightText = desiredWeightText ?? string.Empty),

                    new ImageButton()
                    {
                        Source = "add_plate_icon.png"
                    }
                    .Column(Column.UserControls)
                    .Row(Row.AddPlateButton)
                    .Fill()
                    .Bind(ImageButton.CommandProperty, getter:(MainPageViewModel vm) => vm.AddBarCommand)
                    .Bind(ImageButton.IsVisibleProperty, getter:(MainPageViewModel vm) => vm.CanSubmit)
                }

            }.Top().Center()
        };
    }
    private enum Row { BarType, BarWeight, DesiredWeight, AddPlateButton }
    private enum Column { UserControls }
}
