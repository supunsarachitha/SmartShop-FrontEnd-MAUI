using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public ICommand LogoutCommand { get; }

        public AppShellViewModel()
        {
            UpdateProfileName();
            LogoutCommand = new AsyncRelayCommand(LogoutAsync);
        }

        public void UpdateProfileName()
        {
            LoggedInUserName = AppConstants.CurrentUser.Name ?? AppConstants.CurrentUser.UserName;
        }

        private async Task LogoutAsync()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
