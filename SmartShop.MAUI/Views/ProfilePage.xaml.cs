using SmartShop.MAUI.ViewModels;

namespace SmartShop.MAUI.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel profilePageViewModel)
	{
		InitializeComponent();
		BindingContext = profilePageViewModel;

    }
}