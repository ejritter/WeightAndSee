namespace WeightAndSee.Pages;

public class GeneralPopupPage : BasePopupPage<GeneralPopupViewModel>
{
    public GeneralPopupPage(GeneralPopupViewModel vm) : base(vm)
    {
    }

    protected override void Build()
    {
        this.Bind(CanBeDismissedByTappingOutsideOfPopupProperty, getter: (GeneralPopupViewModel vm) => vm.IsDismissable);

        Content = new VerticalStackLayout()
        {
            Spacing = 3,
            Padding = 3,
            Margin = 3,

            Children =
            {
                new Label()
                    .Bind(Label.TextProperty, getter:(GeneralPopupViewModel vm) => vm.Title)
                    .Center()
                    .Top(),

                new Label()
                     .Bind(Label.TextProperty, getter:(GeneralPopupViewModel vm) => vm.Message)
                     .Center()
                     .CenterHorizontal(),
                new HorizontalStackLayout()
                {
                    Spacing = 3,
                    Padding = 3,
                    Margin = 3,
                    Children =
                    {

                        new  Button()
                            .Text("OK")
                            .Bind(Button.CommandProperty, getter: (GeneralPopupViewModel vm) => vm.OkayClickedCommand)
                            .Center()
                            .Bottom(),

                        new Button()
                            .Text("Cancel")
                            .Bind(Button.CommandProperty, getter:(GeneralPopupViewModel vm) => vm.CancelClickedCommand)
                            .Center()
                            .Bottom()
                    }
                }.Center()


            }
        };
    }
}
