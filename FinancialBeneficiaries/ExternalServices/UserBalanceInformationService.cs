using FinancialManagementServices.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancialManagementServices.ExternalServices
{
    public class UserBalanceInformationService : IUserBalanceInformationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IUserBalanceInformationService> _logger;

        public UserBalanceInformationService(IHttpClientFactory httpClientFactory, ILogger<IUserBalanceInformationService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async  Task<UserBalanceInformation> GetUserBalanceInformationAsync(int userId)
        {
            var url = string.Format("https://localhost:32784/UserBalance/GetUserBalance?userId", userId);

            var httpRequestMessage = new HttpRequestMessage(
           HttpMethod.Get,
           url);
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                UserBalanceInformation userBalanceInformation = await JsonSerializer.DeserializeAsync<UserBalanceInformation>(contentStream);
                return userBalanceInformation;
            }
            else
            {
                _logger.LogError("Error while getting user balance information");
                return null;
            }

        }
    }
}
