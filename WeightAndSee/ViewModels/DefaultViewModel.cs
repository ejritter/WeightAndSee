namespace WeightAndSee.ViewModels;

public partial class DefaultViewModel : BaseViewModel
{
    private readonly IPopupService _popupService;

    [ObservableProperty]
    private string title = "Welcome to MVVM!!";

    [ObservableProperty]
    private string submissionText = string.Empty;

    [ObservableProperty]
    private string popupText = string.Empty;

    public string Placeholder { get; init; } = "Press enter to change title.";
    public DefaultViewModel(IPopupService popupService) : base(popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    private void SetTitle()
    {
        if (SubmissionText == string.Empty)
        {
            return;
        }

        Title = SubmissionText;
        SubmissionText = string.Empty;
    }

    [RelayCommand]
    private async void PageLoaded()
    {
        ShowPopupAsync(Title, "This is a popup page!", false);
    }


    [RelayCommand]
    private async void DisplayPopupAndClickToCancel()
    {
        ShowPopupAsync("General Popup!", "You can click outside of this popup", true);
    }

    [RelayCommand]
    private async void DisplayPopupWithMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }
        ShowPopupAsync("Custom Popup!", $"Your message:{message}. Press okay or cancel to dismiss.", false);
        PopupText = string.Empty;
    }

    [RelayCommand]
    private async void DisplayPopupCannotClickOutside()
    {
        ShowPopupAsync("General Popup!", "Click okay or cancel to dismiss. This cannot be clicked outside.", false);
    }
    private async void ShowPopupAsync(string title, string message, bool isDismissable)
    {
        var _ = await _popupService.ShowPopupAsync<GeneralPopupViewModel>(
                onPresenting: vm =>
                {
                    vm.Title = title;
                    vm.Message = message;
                    vm.IsDismissable = isDismissable;

                    vm.ClosePopup += (sender, result) =>
                    {
                        if (result)
                        {
                            //user clicked ok
                        }
                        else
                        {
                            //user clicked cancel
                        }
                        _popupService.ClosePopupAsync();
                    };
                });
    }
}
