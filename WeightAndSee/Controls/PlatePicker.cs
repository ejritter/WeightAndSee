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
                        .Text("Plate Store.")
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
                                    SelectionMode = SelectionMode.Single,
                                    ItemsLayout = new GridItemsLayout(span:1, orientation: ItemsLayoutOrientation.Horizontal) 
                                                       {
                                                            HorizontalItemSpacing = 2
                                                       }
                                }
                                    .Assign(out CollectionView availablePlatesCv)
                                    .Bind(CollectionView.ItemsSourceProperty, getter:(MainPageViewModel vm) => vm.KiloPlateModels)
                                    .Bind(CollectionView.SelectionChangedCommandProperty, getter:(MainPageViewModel vm) => vm.SetPlateToAvailableCommand)
                                    .Bind(CollectionView.SelectionChangedCommandParameterProperty,source:availablePlatesCv)
                                    .ItemTemplate(new  AvailablePlatesView())
                        }
                    },
                     new Label()
                        .Text($"Plates Available to you.\nIf you don't have access to a plate, tap it here and it will be removed.\nTap it in the plate store to add it back.")
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
                                    SelectionMode = SelectionMode.Single,
                                    ItemsLayout = new GridItemsLayout(span:1, orientation: ItemsLayoutOrientation.Horizontal) 
                                                       {
                                                            HorizontalItemSpacing = 2
                                                       },
                                }
                                    .Assign(out CollectionView platesAvailableToYouCv)
                                    .Bind(CollectionView.ItemsSourceProperty, getter:(MainPageViewModel vm) => vm.PlatesAvailableToYou)
                                    .Bind(CollectionView.SelectionChangedCommandProperty, getter:(MainPageViewModel vm) => vm.SetPlateToUnavailableCommand)
                                    .Bind(CollectionView.SelectionChangedCommandParameterProperty,source:platesAvailableToYouCv)
                                    .ItemTemplate(new  AvailablePlatesView())
                                    
                        }
                    }
                }
            }.Center()
        };
            
    }
}
