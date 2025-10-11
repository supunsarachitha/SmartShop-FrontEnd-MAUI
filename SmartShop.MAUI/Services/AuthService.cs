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
                    throw new InvalidOperationException($"The API service returned a null response for URL: {url}");
                
                return response;
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                throw new ApplicationException($"An error occurred while logging in. URL: {url}", ex);
            }
        }
    }
}
