using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class CustomApiHealthCheck : IHealthCheck
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string ApiUrl = "https://your-api-endpoint.com/health"; // Replace with your real endpoint

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiUrl, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy("API is healthy.");
            }
            else
            {
                return HealthCheckResult.Unhealthy($"API returned status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            return HealthCheckResult.Unhealthy("API request failed.", ex);
        }
    }
}