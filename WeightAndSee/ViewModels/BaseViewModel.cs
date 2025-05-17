namespace WeightAndSee.ViewModels;

public abstract partial class BaseViewModel: ObservableObject
{
    protected BaseViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }
    private readonly IPopupService _popupService;

    [ObservableProperty]
    private bool _confirmOrDenied = false;
    public async Task<bool> ShowPopupAsync(string title, string message, bool isDismissable)
    {
        var results = await _popupService.ShowPopupAsync<GeneralPopupViewModel>(
            onPresenting: vm =>
            {
                vm.Title = title;
                vm.Message = message;
                vm.IsDismissable = isDismissable;

                vm.ClosePopup += (sender, result) =>
                    {
                        if (result)
                        {
                            ConfirmOrDenied = true;
                        }
                        else
                        {
                            ConfirmOrDenied = false;
                        }
                        _popupService.ClosePopupAsync();
                    };
                });

        return ConfirmOrDenied;
    }

}