
namespace WeightAndSee.Pages
{
    public abstract class BasePopupPage<TViewModel> : Popup where TViewModel : BasePopupViewModel
    {
        protected BasePopupPage(TViewModel vm)
        {
#if DEBUG
            HotReloadService.UpdateApplicationEvent += ReloadUI;
#endif
            BindingContext = vm;
            var appShell = Application.Current.Windows.FirstOrDefault(windows => windows.Page is AppShell);
            if (appShell is null == false)
            {
                Window.Width = appShell.Width * 0.7;
                Window.Height = appShell.Width * 0.7;
            }
            Build();

        }

        private void ReloadUI(Type[] obj)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Build();
            });
        }
        protected abstract void Build();

        protected override Task OnClosed(object? result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
        {
#if DEBUG
            HotReloadService.UpdateApplicationEvent -= ReloadUI;
#endif

            return base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
        }



    }
}
