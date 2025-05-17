using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightAndSee.Controls;

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
               Children =
                {
                    new BarPicker()
                        .Center()
                }
            }
        };
    }
}
