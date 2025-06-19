namespace WeightAndSee.Pages;
public abstract partial class BasePopupPage<TViewModel> : ContentView where TViewModel : new()
{
    public BasePopupPage(TViewModel vm)
    {
        BindingContext = vm;
    }
}
