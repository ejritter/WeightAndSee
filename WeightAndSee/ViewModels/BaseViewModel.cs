
namespace WeightAndSee.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    public BaseViewModel(IPopupService popupService)
    {
        PopupService = popupService;
    }

    protected readonly IPopupService PopupService;
    

    public async Task<bool> ShowPopupAsync(string title, string message, bool isDismissable)
    {
        var queryAttributes = new Dictionary<string, object>
        {
            [nameof(BasePopupViewModel.Title)] = title,
            [nameof(BasePopupViewModel.Message)] = message
        };

        var results = await this.PopupService.ShowPopupAsync<GeneralPopupViewModel>(
                shell: Shell.Current,
                options: new PopupOptions { CanBeDismissedByTappingOutsideOfPopup = isDismissable },
                shellParameters: queryAttributes);
       if (results is not null &&
                results is IPopupResult<bool> userResults)
        {
            return userResults.Result;
        }
       else
        {
            return false;
        }
    }
}