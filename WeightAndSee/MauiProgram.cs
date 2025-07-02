namespace WeightAndSee;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            //.UseMauiCommunityToolkitMarkup()//can probably remove since we using xaml again
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<App>();

        builder.Services.AddSingleton<IWeightConversionService, WeightConversionService>();
        builder.Services.AddSingleton<IViewCreatorService, ViewCreatorService>();

        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransientPopup<GeneralPopupPage, GeneralPopupViewModel>();

        //popupservice is baked in DI, don't need to add anything for it in the builder.
        return builder.Build();
    }
}
