using CommunityToolkit.Mvvm.ComponentModel;
using SmartShop.MAUI.Helpers;
using SmartShop.MAUI.Models;

namespace SmartShop.MAUI.ViewModels
{
    public class ProfilePageViewModel : ObservableObject
    {
        private User _user = new User();

        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ProfilePageViewModel()
        {
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            User = AppConstants.CurrentUser;
        }
    }
}
