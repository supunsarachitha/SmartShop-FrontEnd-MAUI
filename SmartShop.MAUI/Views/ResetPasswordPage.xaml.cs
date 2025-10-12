namespace SmartShop.MAUI.Views;

public partial class ResetPasswordPage : ContentPage
{
	public ResetPasswordPage()
	{
		InitializeComponent();

        // Set Shell.NavBarIsVisible based on the platform
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            Shell.SetNavBarIsVisible(this, false);
        }
        else
        {
            Shell.SetNavBarIsVisible(this, true);
        }
    }
}