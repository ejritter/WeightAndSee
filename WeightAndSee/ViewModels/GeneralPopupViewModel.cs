namespace WeightAndSee.ViewModels;

public partial class GeneralPopupViewModel : BasePopupViewModel
{
    public GeneralPopupViewModel()
    {

    }


    [ObservableProperty]
    string title = string.Empty;

    [ObservableProperty]
    private string message = string.Empty;

    [ObservableProperty]
    private bool isDismissable = true;



}
