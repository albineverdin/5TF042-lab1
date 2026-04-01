using AdSystem.Models;

namespace AdSystem.Services;

public interface IAdService
{
    Task<SubscriberResponse?> LookupSubscriberAsync(string subscriptionNumber);
    Task<IEnumerable<Ad>> GetAllAdsAsync();
    Task<bool> DeleteAdAsync(int id);
    Task<IEnumerable<SubscriberRecord>> GetAllSubscribersAsync();
    Task<SubscriberRecord?> CreateSubscriberAsync(SubscriberRecord subscriber);
    Task<bool> DeleteSubscriberAsync(int id);
    Task<string?> ExportSubscribersXmlAsync();
    Task<int?> ImportSubscribersXmlAsync(string xml);
    Task<ExchangeRates?> GetExchangeRatesAsync();
    Task<Ad> CreateSubscriberAdAsync(string subscriptionNumber, string name, string phone,
        string address, string postalCode, string city,
        string title, string content, decimal itemPrice);
    Task<Ad> CreateCompanyAdAsync(string organisationNumber, string name, string phone,
        string address, string postalCode, string city,
        string invoiceAddress, string invoicePostalCode, string invoiceCity,
        string title, string content, decimal itemPrice);
}
