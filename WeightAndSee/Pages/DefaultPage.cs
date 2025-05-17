namespace WeightAndSee.Pages;

public class DefaultPage : BasePage<DefaultViewModel>
{
    public DefaultPage(DefaultViewModel vm) : base(vm)
    {
        this.Behaviors(new EventToCommandBehavior()
        {
            EventName = nameof(Loaded),
            Command = vm.PageLoadedCommand
        });
    }
    protected override void Build()
    {
        this.Bind(ContentPage.TitleProperty, getter: (DefaultViewModel vm) => vm.Title);
        Content = new VerticalStackLayout()
        {
            Spacing = 3,
            Margin = 3,
            Padding = 3,

            Children =
            {
              new Entry()
               .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                           getter: (DefaultViewModel vm) => vm.SubmissionText,
                           setter: (DefaultViewModel vm, string? submissionText) => vm.SubmissionText = submissionText ?? string.Empty)
               .Bind(Entry.ReturnCommandProperty,
                           getter: (DefaultViewModel vm) => vm.SetTitleCommand)
               .Bind(Entry.PlaceholderProperty, getter: (DefaultViewModel vm) => vm.Placeholder),

              new Button()
                .Text("Popup: Cannot be dismissed.")
                .Bind(Button.CommandProperty, getter:(DefaultViewModel vm) => vm.DisplayPopupCannotClickOutsideCommand)
                .Center()
                .Bottom(),

              new Entry()
                .Placeholder("Custom popup message")
                .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                          getter:(DefaultViewModel vm) => vm.PopupText,
                                          setter:(DefaultViewModel vm, string? entryText) => vm.PopupText = entryText)
                .Center()
                .Bottom(),

              new Button()
                .Text("Popup: Add your message!")
                .Bind(Button.CommandProperty, getter:(DefaultViewModel vm) => vm.DisplayPopupWithMessageCommand)
                .Bind(Button.CommandParameterProperty, getter:(DefaultViewModel vm) => vm.PopupText)
                .Center()
                .Bottom(),

              new Button()
               .Text("Popup: Click through to dismiss")
               .Bind(Button.CommandProperty, getter:(DefaultViewModel vm) => vm.DisplayPopupAndClickToCancelCommand)
               .Center()
               .Bottom()


            }

        };
    }
}
