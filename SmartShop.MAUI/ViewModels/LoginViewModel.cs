using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using SmartShop.MAUI.Models.Responses;
using SmartShop.MAUI.Services;
using SmartShop.MAUI.Views;
using System.Net.Http;
using System.Windows.Input;

namespace SmartShop.MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string alertMessage = string.Empty;

        [ObservableProperty]
        private string appVersion;

        [ObservableProperty]
        private string serverStatus = "Checking...";

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
            //_httpClient = new HttpClient();
            appVersion = VersionTracking.CurrentVersion;

            // Start checking server status
            CheckServerStatus();
        }

        private async void CheckServerStatus()
        {
            while (true)
            {
                try
                {
                    // Replace with your server health check endpoint
                    //var response = await _httpClient.GetAsync("https://yourserver.com/health");
                    //serverStatus = response.IsSuccessStatusCode ? "Online" : "Offline";

                    serverStatus = "Online";
                }
                catch
                {
                    serverStatus = "Offline";
                }

                // Wait for 30 seconds before checking again
                await Task.Delay(30000);
            }
        }

        [RelayCommand]
        private async Task Login()
        {
            Console.WriteLine("Login executed");

            if (IsBusy) return;

            IsBusy = true;
            AlertMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    AlertMessage = $"Login failed: Username cannot be empty.";
                } 
                else if (string.IsNullOrWhiteSpace(Password))
                {
                    AlertMessage = $"Login failed: Password cannot be empty.";
                }
                else
                {
                    var result = await _authService.LoginAsync<LoginResponse>(Username, Password);

                    if (result != null && result.Success && result.Data != null)
                    {
                        // Extract the token
                        string token = result.Data.Token;

                        Console.WriteLine($"Token: {token}");

                        // Navigate to HomePage.xaml
                        await Shell.Current.GoToAsync("//HomePage", true);


                        //Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
                    }
                    else
                    {
                        AlertMessage = result?.Message ?? "Invalid username or password.";
                    }
                }
                    
            }
            catch (Exception ex)
            {
                AlertMessage = $"Login failed";
                Console.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task ForgotPassword()
        { 
            // Navigate to ResetPasswordPage 
            await Shell.Current.GoToAsync(nameof(ResetPasswordPage), true);
             
        }
         
    }
}
