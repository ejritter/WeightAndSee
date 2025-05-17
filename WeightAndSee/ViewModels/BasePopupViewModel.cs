namespace WeightAndSee.ViewModels;

public abstract partial class BasePopupViewModel : ObservableObject
{
    public BasePopupViewModel()
    {

    }

    public event EventHandler<bool>? ClosePopup;

    [RelayCommand]
    protected void OkayClicked()
    {
        ClosePopup?.Invoke(this, true);
    }

    [RelayCommand]
    protected void CancelClicked()
    {
        ClosePopup?.Invoke(this, false);
    }
}
