using CommunityToolkit.Mvvm.ComponentModel;
using SmartShop.MAUI.Helpers;

namespace SmartShop.MAUI.ViewModels
{
    public class AppShellViewModel : ObservableObject
    {
        private string _loggedInUserName = string.Empty;

        public string LoggedInUserName
        {
            get => _loggedInUserName;
            set => SetProperty(ref _loggedInUserName, value);
        }

        public AppShellViewModel()
        {
            UpdateProfileName();
        }

        public void UpdateProfileName()
        {
            LoggedInUserName = AppConstants.CurrentUser.Name ?? AppConstants.CurrentUser.UserName;
        }
    }
}
