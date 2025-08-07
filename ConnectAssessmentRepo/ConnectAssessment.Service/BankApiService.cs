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
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<bool> TransferFundsAsync(string accountNumber, decimal amount)
        {
            var url = _config["BankApi:BaseUrl"];
            var response = await _httpClient.PostAsJsonAsync(url, new { account = accountNumber, amount });
            return response.IsSuccessStatusCode;
        }
    }
}
