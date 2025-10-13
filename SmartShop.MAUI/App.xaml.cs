using SmartShop.MAUI.ViewModels;

namespace SmartShop.MAUI;

public partial class App : Application
{
	private AppShellViewModel _appShellViewModel;


    public App(AppShellViewModel homePageViewModel)
	{
		InitializeComponent();
        _appShellViewModel = homePageViewModel;

    }

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell(_appShellViewModel));
	}
}