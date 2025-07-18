
namespace WeightAndSee.ViewModels;

public abstract partial class BaseViewModel(IPopupService popupService, IWeightConversionService weightConversionService,
                                            IViewCreatorService viewCreatorService, IToastService toaster) : ObservableObject
{


    protected readonly IPopupService PopupService = popupService;
    protected readonly IWeightConversionService WeightConversionService = weightConversionService;
    protected readonly IViewCreatorService ViewCreatorService = viewCreatorService;
    protected readonly IToastService ToastService = toaster;
    public AppTheme CurrentTheme = App.Current.RequestedTheme;
    public async Task<bool> ShowPopupAsync(string title, string message, bool isDismissable = true)
    {
        var queryAttributes = new Dictionary<string, object>
        {
            [nameof(BasePopupViewModel.Title)] = title,
            [nameof(BasePopupViewModel.Message)] = message
        };

        var results = await this.PopupService
                                .ShowPopupAsync<GeneralPopupViewModel>(
                                                                        shell: Shell.Current,
                                                                        options: new PopupOptions
                                                                        {
                                                                            CanBeDismissedByTappingOutsideOfPopup = isDismissable,
                                                                            PageOverlayColor = CurrentTheme == AppTheme.Light ? Colors.White : Colors.Black,
                                                                        },
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