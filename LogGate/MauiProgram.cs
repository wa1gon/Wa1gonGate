using CommunityToolkit.Maui;
using LogGate.View;
using LogGate.ViewModel;
using Microsoft.Extensions.Logging;
using SqlServerRepo;

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
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<IQSORepo, SqlRepo>();
        builder.Services.AddSingleton<SettingManager>();
        builder.Services.AddTransient<MainViewModel>();
        //builder.Services.AddTransient<QsoViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
