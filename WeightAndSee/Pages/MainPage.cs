namespace WeightAndSee.Pages;
public class MainPage : BasePage<MainPageViewModel>
{
    public MainPage(MainPageViewModel vm) : base(vm)
    {
        Build();
        this.Background = Colors.White;
        
    }

    protected override void Build()
    {
        this.Content = new ScrollView()
        {
            Margin = 5,
            Padding = 5,

            Content = new VerticalStackLayout()
            {
                Spacing = 5,
                Margin = 5,
                Padding = 5,
                Children =
                {
                    new BarPicker()
                        .Center()
                        .Background(Color.FromArgb("#FAFAFA")),
                    new ContentView()
                        .Bind(ContentView.ContentProperty, getter:(MainPageViewModel vm) => vm.BarReportView)
                        .Bind(ContentView.IsVisibleProperty, getter:(MainPageViewModel vm) => vm.ShowReport)
                        .Center()
                        .Background(Color.FromArgb("#FAFAFA")),
                   new ContentView()
                        .Bind(ContentView.ContentProperty, getter:(MainPageViewModel vm) => vm.BarView)
                        .Bind(ContentView.IsVisibleProperty, getter:(MainPageViewModel vm) => vm.ShowBar)
                        .Center()
                        .Background(Color.FromArgb("#FAFAFA")),
                }
            }
        };
    }
}
