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

    public async Task<IEnumerable<SubscriberRecord>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("api/subscribers");

        if (!response.IsSuccessStatusCode)
            return Enumerable.Empty<SubscriberRecord>();

        return await response.Content.ReadFromJsonAsync<IEnumerable<SubscriberRecord>>()
               ?? Enumerable.Empty<SubscriberRecord>();
    }

    public async Task<SubscriberRecord?> CreateAsync(SubscriberRecord subscriber)
    {
        var response = await _httpClient.PostAsJsonAsync("api/subscribers", subscriber);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<SubscriberRecord>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/subscribers/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<string?> ExportXmlAsync()
    {
        var response = await _httpClient.GetAsync("api/subscribers/export");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<int?> ImportXmlAsync(string xml)
    {
        var content = new StringContent(xml, System.Text.Encoding.UTF8, "application/xml");
        var response = await _httpClient.PostAsync("api/subscribers/import", content);
        if (!response.IsSuccessStatusCode) return null;
        var result = await response.Content.ReadFromJsonAsync<ImportResult>();
        return result?.Inserted;
    }

    private record ImportResult(int Inserted);
}
