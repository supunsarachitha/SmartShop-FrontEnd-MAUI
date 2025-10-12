 

namespace SmartShop.MAUI.Services
{

    public class ServerStatusService
    {
        private readonly ApiService _apiService;
        private readonly string _baseUrl;

        public ServerStatusService(ApiService apiService, string baseUrl)
        {
            _apiService = apiService;
            _baseUrl = baseUrl;
        }

        public async Task<bool> IsServerOnlineAsync()
        {
            var url = $"{_baseUrl}/api/Status";
            try
            {
                var response = await _apiService.GetAsync<object>(url);
                return response != null; // If we get a response, the server is online
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex);
                return false; 
            }
        }

    }
}
