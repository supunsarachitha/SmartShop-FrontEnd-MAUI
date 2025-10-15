using SmartShop.MAUI.Views;
using SmartShop.MAUI.ViewModels;

namespace SmartShop.MAUI;

public partial class AppShell : Shell
{
    public AppShell(AppShellViewModel appShellViewModel)
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
        BindingContext = appShellViewModel;

        if (appShellViewModel.IsUserLoggedIn())
        {
            // Navigate to HomePage if the user is already logged in
            GoToAsync("//HomePage");
        }
        else
        {
            // Navigate to LoginPage if the user is not logged in
            GoToAsync("//LoginPage");
        }
    }
  
}
