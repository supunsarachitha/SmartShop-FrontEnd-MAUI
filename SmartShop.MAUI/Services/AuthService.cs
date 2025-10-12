using SmartShop.MAUI.Models.Requests;
using SmartShop.MAUI.Models.Responses;


namespace SmartShop.MAUI.Services
{
    public class AuthService
    {
        private readonly ApiService _apiService;
        private readonly string _baseUrl;

        public AuthService(ApiService apiService, string baseUrl)
        {
            _apiService = apiService;
            _baseUrl = baseUrl;
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
                var response = await _apiService.PostAsync<LoginRequest, ApplicationResponse<T>>(url, data);

                if (response == null)
                {
                    return new ApplicationResponse<T>
                    {
                        Success = false,
                        Message = "No response from server.",
                        Data = default,
                        Errors = new List<ErrorDetail>(), // Initialize an empty list instead of null
                        StatusCode = null
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                Console.WriteLine(ex); // Use the exception to avoid CS0168 warning
                return new ApplicationResponse<T>
                {
                    Success = false,
                    Message = "An error occurred while logging in",
                    Data = default,
                    Errors = new List<ErrorDetail>(), // Initialize an empty list instead of null
                    StatusCode = null
                };
            }
        }

        public async Task SendPasswordResetLinkAsync(string username)
        {
            var url = $"{_baseUrl}/api/Auth/resetPassword";

            var data = new { UserName = username }; // Payload for the request

            try
            {
                var response = await _apiService.PostAsync<object, ApplicationResponse<object>>(url, data);

                if (response == null || !response.Success)
                {
                    throw new InvalidOperationException(response?.Message ?? "Failed to send password reset link.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow
                Console.WriteLine($"Error sending password reset link: {ex.Message}");
                throw new ApplicationException("An error occurred while sending the password reset link.", ex);
            }
        }
    }
}
