using SmartShop.MAUI.Views;

namespace SmartShop.MAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
	}
}
