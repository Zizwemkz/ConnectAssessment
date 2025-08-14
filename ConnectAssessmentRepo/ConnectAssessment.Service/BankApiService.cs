using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ConnectAssessment.Common.Service;
using Microsoft.Extensions.Configuration;

namespace ConnectAssessment.Service
{
    public class BankApiService : IBankApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public BankApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<bool> TransferFundsAsync(string accountNumber, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number must not be empty.", nameof(accountNumber));

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

            var url = _config["BankApi:BaseUrl"];
            if (string.IsNullOrWhiteSpace(url))
                throw new InvalidOperationException("Bank API base URL is not configured.");

            try
            {
                var payload = new { account = accountNumber, amount };
                var response = await _httpClient.PostAsJsonAsync(url, payload);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
