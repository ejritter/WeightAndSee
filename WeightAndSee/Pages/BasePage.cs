namespace WeightAndSee.Pages;

public abstract class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel
{
    public BasePage(TViewModel vm)
    {
        BindingContext = vm;
    }
}
