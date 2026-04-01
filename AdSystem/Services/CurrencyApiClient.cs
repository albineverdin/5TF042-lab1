using System.Net.Http.Json;
using AdSystem.Models;

namespace AdSystem.Services;

public class CurrencyApiClient
{
    private readonly HttpClient _httpClient;

    public CurrencyApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRates?> GetRatesFromSekAsync()
    {
        var response = await _httpClient.GetAsync("latest?from=SEK&to=EUR,USD,GBP");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ExchangeRates>();
    }
}