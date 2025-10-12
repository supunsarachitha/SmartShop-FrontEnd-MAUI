using Microsoft.Extensions.Logging;
using SmartShop.MAUI.Helpers;
using SmartShop.MAUI.Services;
using SmartShop.MAUI.ViewModels;
using CommunityToolkit.Maui;

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
            return new AuthService(apiService, AppConstants.ApiBaseUrl);
        });
        builder.Services.AddSingleton<ServerStatusService>(sp =>
        {
            var serverStatusService = sp.GetRequiredService<ApiService>();
            return new ServerStatusService(serverStatusService, AppConstants.ApiBaseUrl);
        });

        // Register view models
        builder.Services.AddTransient<LoginViewModel>();
		return builder.Build();
	}
}
