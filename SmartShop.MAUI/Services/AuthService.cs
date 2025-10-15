using Microsoft.Extensions.Logging;
using SmartShop.MAUI.Helpers;
using SmartShop.MAUI.Models.Requests;
using SmartShop.MAUI.Models.Responses;
using System.Text.Json;


namespace SmartShop.MAUI.Services
{
    public class AuthService
    {
        private readonly ApiService _apiService;
        private readonly string _baseUrl;
        private readonly ILogger<AuthService> _logger;

        public AuthService(ApiService apiService, string baseUrl, ILogger<AuthService> logger)
        {
            _apiService = apiService;
            _baseUrl = baseUrl;
            _logger = logger;
        }

        public async Task<ApplicationResponse<T>> LoginAsync<T>(string username, string password)
        {
            var url = $"{_baseUrl}/api/Auth/login";

            var data = new LoginRequest
            {
                UserName = username,
                Password = password
            };

            try
            {
                _logger.LogInformation("Attempting to log in user: {Username}", username);

                var response = await _apiService.PostAsync<LoginRequest, ApplicationResponse<T>>(url, data);

                if (response == null)
                {
                    _logger.LogWarning("No response from server for login attempt.");
                    return new ApplicationResponse<T>
                    {
                        Success = false,
                        Message = "No response from server.",
                        Data = default,
                        Errors = new List<ErrorDetail>(),
                        StatusCode = null
                    };
                }

                _logger.LogInformation("Login successful for user: {Username}", username);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user: {Username}", username);
                return new ApplicationResponse<T>
                {
                    Success = false,
                    Message = "An error occurred while logging in",
                    Data = default,
                    Errors = new List<ErrorDetail>(),
                    StatusCode = null
                };
            }
        }

        public async Task SendPasswordResetLinkAsync(string username)
        {
            var url = $"{_baseUrl}/api/Auth/resetPassword";

            var data = new { UserName = username };

            try
            {
                _logger.LogInformation("Sending password reset link to user: {Username}", username);

                var response = await _apiService.PostAsync<object, ApplicationResponse<object>>(url, data);

                if (response == null || !response.Success)
                {
                    _logger.LogWarning("Failed to send password reset link to user: {Username}", username);
                    throw new InvalidOperationException(response?.Message ?? "Failed to send password reset link.");
                }

                _logger.LogInformation("Password reset link sent successfully to user: {Username}", username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending password reset link to user: {Username}", username);
                throw new ApplicationException("An error occurred while sending the password reset link.", ex);
            }
        }

        /// <summary>
        /// Determines whether there are previously stored user credentials available.
        /// </summary>
        /// <remarks>This method checks for the presence of a stored user authentication response and
        /// validates its contents. If valid credentials are found, the authentication token and user information are
        /// loaded into the application constants.</remarks>
        /// <returns><see langword="true"/> if valid user credentials are found and loaded; otherwise, <see langword="false"/>.</returns>
        public bool HasPreviousCredentionals()
        {
            var userAuthResponseJson = PreferenceHelper.GetPreference("UserAuthenticationResponse", string.Empty);

            if (!string.IsNullOrEmpty(userAuthResponseJson))
            {
                var userAuthResponse = JsonSerializer.Deserialize<UserAuthenticationResponse>(userAuthResponseJson);

                if (userAuthResponse != null && userAuthResponse.Token != null && userAuthResponse.User != null)
                {
                    AppConstants.AuthToken = userAuthResponse.Token;
                    AppConstants.CurrentUser = userAuthResponse.User;

                    return true;
                }
            }

            return false;

        }
    }
}
