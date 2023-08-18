using CommunityToolkit.Maui;
using LogGate.ViewModel;
using Microsoft.Extensions.Logging;

namespace LogGate;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<SettingManager>();
        builder.Services.AddTransient<MainViewModel>();
        //builder.Services.AddTransient<QsoViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
