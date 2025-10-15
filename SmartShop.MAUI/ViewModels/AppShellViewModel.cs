using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartShop.MAUI.Helpers;
using SmartShop.MAUI.Models.Responses;
using SmartShop.MAUI.Services;
using System.Windows.Input;

namespace SmartShop.MAUI.ViewModels
{
    public class AppShellViewModel : ObservableObject
    {
        private string _loggedInUserName = string.Empty;
        private readonly AuthService _authService;

        public string LoggedInUserName
        {
            get => _loggedInUserName;
            set => SetProperty(ref _loggedInUserName, value);
        }

        public ICommand LogoutCommand { get; }

        public AppShellViewModel(AuthService authService)
        {
            _authService = authService;
            UpdateProfileName();
            LogoutCommand = new AsyncRelayCommand(LogoutAsync);
            
        }

        public void UpdateProfileName()
        {
            LoggedInUserName = AppConstants.CurrentUser.Name ?? AppConstants.CurrentUser.UserName;
        }

        private async Task LogoutAsync()
        {
            Preferences.Remove("UserAuthenticationResponse");
            await Shell.Current.GoToAsync("//LoginPage");

        }

        public bool IsUserLoggedIn()
        {
            if (_authService.HasPreviousCredentionals())
            {
                UpdateProfileName();
                return true;
            }

            return false;
        }



    }
}
