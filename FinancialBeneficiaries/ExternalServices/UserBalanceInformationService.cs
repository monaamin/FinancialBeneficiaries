using FinancialManagementServices.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FinancialManagementServices.ExternalServices
{
    public class UserBalanceInformationService : IUserBalanceInformationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IUserBalanceInformationService> _logger;
        private readonly IConfiguration Configuration;
        public UserBalanceInformationService(IHttpClientFactory httpClientFactory, ILogger<IUserBalanceInformationService> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            Configuration = configuration;
        }
        public async Task<UserBalanceInformation> GetUserBalanceInformationAsync(int userId)
        {
            var client = _httpClientFactory.CreateClient();
            var baseURL = Configuration["UserBalanceSetting:MainURL"];
            var url = string.Format(baseURL + "GetUserBalance?userId={0}", userId);

            HttpRequestMessage request = new HttpRequestMessage();

            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Get;
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<UserBalanceInformation>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {

                throw new HttpRequestException(response.ReasonPhrase);

            }
        }

        public async Task DebitUserBalanceAsync(UserBalanceInformation debitInfo)
        {
            var client = _httpClientFactory.CreateClient();
            var baseURL = Configuration["UserBalanceSetting:MainURL"];
            var url = string.Format(baseURL + "CreditUserBalance");

            HttpRequestMessage request = new HttpRequestMessage();

            request.RequestUri = new Uri(url);

            var debitInfoDetails = new StringContent(JsonSerializer.Serialize(debitInfo),Encoding.UTF8, Application.Json);
            using var httpResponseMessage = await client.PostAsync("/api/TodoItems", debitInfoDetails);
            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
