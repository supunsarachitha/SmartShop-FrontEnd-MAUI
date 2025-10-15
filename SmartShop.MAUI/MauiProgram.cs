using Microsoft.Extensions.Logging;
using SmartShop.MAUI.Helpers;
using SmartShop.MAUI.Services;
using SmartShop.MAUI.ViewModels;
using CommunityToolkit.Maui;
using SmartShop.MAUI.Views;

namespace SmartShop.MAUI;

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
                fonts.AddFont("Font Awesome 7 Brands-Regular-400.otf", "FontAwesomeBrands");
                fonts.AddFont("Font Awesome 7 Free-Regular-400.otf", "FontAwesomeRegular");
                fonts.AddFont("Font Awesome 7 Free-Solid-900.otf", "FontAwesomeSolid");
            });

        builder.UseMauiCommunityToolkit();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        // Register services
        builder.Services.AddSingleton<ApiService>();

        builder.Services.AddSingleton<AuthService>(sp =>
        {
            var apiService = sp.GetRequiredService<ApiService>();
            var logger = sp.GetRequiredService<ILogger<AuthService>>();
            return new AuthService(apiService, AppConstants.ApiBaseUrl, logger);
        });

        builder.Services.AddSingleton<ServerStatusService>(sp =>
        {
            var serverStatusService = sp.GetRequiredService<ApiService>();
            return new ServerStatusService(serverStatusService, AppConstants.ApiBaseUrl);
        });

        // Register view models
        builder.Services.AddTransient<LoginViewModel>(); 
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddTransient<HomePageViewModel>();
         

        return builder.Build();
	}
}
