namespace WeightAndSee.ViewModels;
public abstract partial class BasePopupViewModel(IPopupService popupService) : ObservableObject, IQueryAttributable
{
    protected readonly IPopupService PopupService = popupService;


    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _message = string.Empty;
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Title = (string)query[nameof(BasePopupViewModel.Title)];
        Message = (string)query[nameof(BasePopupViewModel.Message)];
    }

    [RelayCommand]
    private async Task OkayClicked()
    {
        await PopupService.ClosePopupAsync(Shell.Current, true);
    }

    [RelayCommand]
    private async Task CancelClicked()
    {
        await PopupService.ClosePopupAsync(Shell.Current, false);
    }
}
