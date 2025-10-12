using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartShop.MAUI.Models.Responses;
using SmartShop.MAUI.Services;
using SmartShop.MAUI.Views;

namespace SmartShop.MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly ServerStatusService _serverStatusService;

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _alertMessage = string.Empty;
        public string AlertMessage
        {
            get => _alertMessage;
            set => SetProperty(ref _alertMessage, value);
        }

        private string _appVersion = string.Empty;
        public string AppVersion
        {
            get => _appVersion;
            set => SetProperty(ref _appVersion, value);
        }

        private string _serverStatus = "Checking...";
        public string ServerStatus
        {
            get => _serverStatus;
            set => SetProperty(ref _serverStatus, value);
        }

        public LoginViewModel(AuthService authService, ServerStatusService serverStatusService)
        {
            _authService = authService;
            _serverStatusService = serverStatusService;
            AppVersion = VersionTracking.CurrentVersion;

            // Start checking server status
            CheckServerStatus();
        }

        /// <summary>
        /// Continuously monitors the server status and updates the <see cref="ServerStatus"/> property.
        /// </summary>
        /// <remarks>This method periodically checks the server's availability by invoking the               
        /// property is updated to "Online" if the server is reachable, or "Offline" otherwise.If an exception
        /// occurs during the status check, the server status is set to "Offline".The method runs
        /// indefinitely, with a delay of 30 seconds between each status check.</remarks>
        private async void CheckServerStatus()
        {
            while (true)
            {
                try
                {

                    var isOnline = await _serverStatusService.IsServerOnlineAsync();
                    if (isOnline)
                        ServerStatus = "Online";
                    else
                        ServerStatus = "Offline";
                }
                catch
                {
                    ServerStatus = "Offline";
                }

                await Task.Delay(30000);
            }
        }

        /// <summary>
        /// Attempts to log in the user using the provided username and password.
        /// </summary>
        /// <remarks>This method validates the username and password, and if valid, initiates an
        /// asynchronous login request. If the login is successful, the user is navigated to the home page. Otherwise,
        /// an appropriate error message is set in the <c>AlertMessage</c> property. The method ensures that the
        /// <c>IsBusy</c> flag is set during the operation to prevent concurrent logins.</remarks>
        /// <returns></returns>
        [RelayCommand]
        private async Task Login()
        {
            if (IsBusy) return;

            IsBusy = true;
            AlertMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    AlertMessage = "Login failed: Username cannot be empty.";
                }
                else if (string.IsNullOrWhiteSpace(Password))
                {
                    AlertMessage = "Login failed: Password cannot be empty.";
                }
                else
                {
                    var result = await _authService.LoginAsync<LoginResponse>(Username, Password);

                    if (result != null && result.Success && result.Data != null)
                    {
                        string? token = result.Data.Token;

                        if (!string.IsNullOrEmpty(token))
                        {
                            await Shell.Current.GoToAsync("//HomePage", true);
                        }
                        else
                        {
                            AlertMessage = "Login failed";
                        }
                    }
                    else
                    {
                        AlertMessage = result?.Message ?? "Invalid username or password.";
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMessage = "Login failed";
                Console.WriteLine(ex);
            }
            finally
            {
                Username = string.Empty;
                Password = string.Empty;
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task ForgotPassword()
        {
            await Shell.Current.GoToAsync(nameof(ResetPasswordPage), true);
        }

        /// <summary>
        /// Sends a password reset link to the user associated with the specified username.
        /// </summary>
        /// <remarks>This method attempts to send a password reset link asynchronously using the provided
        /// authentication service. If the operation is successful, a confirmation message is set. If the operation
        /// fails, an error message is set.</remarks>
        /// <returns></returns>
        [RelayCommand]
        private async Task ResetPassword()
        {
            try
            {
                await _authService.SendPasswordResetLinkAsync(Username);

                AlertMessage = "Password reset link sent successfully.";
            }
            catch (Exception ex)
            {
                AlertMessage = "Failed to send password reset link.";
                Console.WriteLine(ex);
            }
        }
    }
}
