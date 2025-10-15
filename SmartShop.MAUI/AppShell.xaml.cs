using SmartShop.MAUI.Views;
using SmartShop.MAUI.ViewModels;

namespace SmartShop.MAUI;

public partial class AppShell : Shell
{
    private readonly AppShellViewModel _appShellViewModel;

    public AppShell(AppShellViewModel appShellViewModel)
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
        BindingContext = appShellViewModel;
        _appShellViewModel = appShellViewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (await _appShellViewModel.IsUserLoggedInAsync())
        {
            // Navigate to HomePage if the user is already logged in
            await GoToAsync("//HomePage");
        }
        else
        {
            // Navigate to LoginPage if the user is not logged in
            await GoToAsync("//LoginPage");
        }
    }
}
