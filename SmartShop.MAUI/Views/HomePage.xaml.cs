using SmartShop.MAUI.ViewModels;

namespace SmartShop.MAUI.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}