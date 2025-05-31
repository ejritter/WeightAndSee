namespace WeightAndSee.Controls;
public class PlatePicker : ContentView
{
    public PlatePicker()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Content = new ScrollView()
        {
            Content = new VerticalStackLayout()
            {
                Spacing = 4,
                Margin = 4,
                Padding = 4,

                Children =
                {
                    new Label()
                        .Text("If a plate is unavailable, unselect it from the list and it won't be loaded on the bar.")
                        .TextColor(Colors.Black),
                    new HorizontalStackLayout()
                    {
                        Spacing = 4,
                        Margin = 4,
                        Padding = 4,

                        Children =
                        {
                                new CollectionView()
                                {
                                    SelectionMode = SelectionMode.Multiple,
                                    ItemsLayout = new GridItemsLayout(span:1, orientation: ItemsLayoutOrientation.Horizontal) 
                                                       {
                                                            HorizontalItemSpacing = 2
                                                       }
                                }
                                    .Assign(out CollectionView cv)
                                    .Bind(CollectionView.ItemsSourceProperty, getter:(MainPageViewModel vm) => vm.KiloPlateModels)
                                    .Bind(CollectionView.SelectedItemsProperty, mode:BindingMode.TwoWay,
                                                                                getter:(MainPageViewModel vm) => vm.SelectedPlates)
                                    .Bind(CollectionView.SelectionChangedCommandProperty, getter:(MainPageViewModel vm) => vm.EnableOrDisablePlateCommand)
                                    .Bind(CollectionView.SelectionChangedCommandParameterProperty,source:cv)
                                    .ItemTemplate(new  AvailablePlatesView())
                        }
                    }
                }
            }.Center()
        };
            
    }
}
