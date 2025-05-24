using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WeightAndSee.Pages;
public class MainPage : BasePage<MainPageViewModel>
{
    public MainPage(MainPageViewModel vm) : base(vm)
    {
        Build();
    }

    protected override void Build()
    {
        this.Content = new ScrollView()
        {
            Content = new VerticalStackLayout()
            {
                Spacing = 5,
                Margin = 5,
                Children =
                {
                    new BarPicker()
                        .Center(),

                    new Label()
                        .Bind(Label.TextProperty, getter:(MainPageViewModel vm) => vm.BarReport)
                        .Bind(Label.IsVisibleProperty, getter:(MainPageViewModel vm) => vm.ShowReport)
                        .Center(),

                   new ContentView()
                        .Bind(ContentView.ContentProperty, getter:(MainPageViewModel vm) => vm.BarView)
                        .Bind(ContentView.IsVisibleProperty, getter:(MainPageViewModel vm) => vm.ShowBar)
                        .Center()
                }
            }
        };
    }
}
