using System.Net.Http.Json;
using AdSystem.Models;

namespace AdSystem.Services;

public class SubscriberApiClient
{
    private readonly HttpClient _httpClient;

    public SubscriberApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SubscriberResponse?> GetBySubscriptionNumberAsync(string subscriptionNumber)
    {
        var response = await _httpClient.GetAsync($"api/subscribers/{subscriptionNumber}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<SubscriberResponse>();
    }
}
