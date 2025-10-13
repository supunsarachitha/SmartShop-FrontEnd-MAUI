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
    }
  
}
