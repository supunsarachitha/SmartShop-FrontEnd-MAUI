using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SmartShop.MAUI.Helpers;
using SmartShop.MAUI.Models.Responses;
using SmartShop.MAUI.Services;
using SmartShop.MAUI.Views;
using System.Reflection.Metadata;

namespace SmartShop.MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly ServerStatusService _serverStatusService;
        private readonly ILogger<LoginViewModel> _logger;

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

        public LoginViewModel(AuthService authService, ServerStatusService serverStatusService, ILogger<LoginViewModel> logger)
        {
            _authService = authService;
            _serverStatusService = serverStatusService;
            _logger = logger;
            AppVersion = VersionTracking.CurrentVersion;

            // Start checking server status
            _ = CheckServerStatus();
        }

        private async Task CheckServerStatus()
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
            await Shell.Current.GoToAsync("//HomePage", true);

            if (IsBusy) return;

            IsBusy = true;
            AlertMessage = string.Empty;

            try
            {
                _logger.LogInformation("Login attempt started for user: {Username}", Username);

                if (string.IsNullOrWhiteSpace(Username))
                {
                    AlertMessage = "Login failed: Username cannot be empty.";
                    _logger.LogWarning("Login failed: Username was empty.");
                }
                else if (string.IsNullOrWhiteSpace(Password))
                {
                    AlertMessage = "Login failed: Password cannot be empty.";
                    _logger.LogWarning("Login failed: Password was empty.");
                }
                else
                {
                    var result = await _authService.LoginAsync<UserAuthenticationResponse>(Username, Password);

                    if (result != null && result.Success && result.Data != null && result.Data.User != null)
                    {
                        string? token = result.Data.Token;

                        if (!string.IsNullOrEmpty(token))
                        {
                            _logger.LogInformation("Login successful for user: {Username}", Username);

                            //store login details.
                            AppConstants.AuthToken = token;
                            AppConstants.CurrentUser = result.Data.User;

                            await Shell.Current.GoToAsync("//HomePage", true);
                        }
                        else
                        {
                            AlertMessage = "Login failed";
                            _logger.LogWarning("Login failed: Token was null or empty.");
                        }
                    }
                    else
                    {
                        AlertMessage = result?.Message ?? "Invalid username or password.";
                        _logger.LogWarning("Login failed: {Message}", result?.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMessage = "Login failed";
                _logger.LogError(ex, "An error occurred during login for user: {Username}", Username);
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
            _logger.LogInformation("Navigating to Forgot Password page.");
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
                _logger.LogInformation("Attempting to send password reset link for user: {Username}", Username);
                await _authService.SendPasswordResetLinkAsync(Username);

                AlertMessage = "Password reset link sent successfully.";
                _logger.LogInformation("Password reset link sent successfully for user: {Username}", Username);
            }
            catch (Exception ex)
            {
                AlertMessage = "Failed to send password reset link.";
                _logger.LogError(ex, "An error occurred while sending password reset link for user: {Username}", Username);
            }
        }
    }
}
