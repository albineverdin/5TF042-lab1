using AdSystem.DAL;
using AdSystem.Models;

namespace AdSystem.Services;

public class AdService : IAdService
{
    private readonly IAdvertiserRepository _advertiserRepository;
    private readonly IAdRepository _adRepository;
    private readonly SubscriberApiClient _apiClient;
    private readonly CurrencyApiClient _currencyClient;

    public AdService(IAdvertiserRepository advertiserRepository, IAdRepository adRepository,
        SubscriberApiClient apiClient, CurrencyApiClient currencyClient)
    {
        _advertiserRepository = advertiserRepository;
        _adRepository = adRepository;
        _apiClient = apiClient;
        _currencyClient = currencyClient;
    }

    public async Task<SubscriberResponse?> LookupSubscriberAsync(string subscriptionNumber)
    {
        return await _apiClient.GetBySubscriptionNumberAsync(subscriptionNumber);
    }

    public async Task<IEnumerable<Ad>> GetAllAdsAsync()
    {
        return await _adRepository.GetAllAsync();
    }

    public async Task<bool> DeleteAdAsync(int id)
    {
        return await _adRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<SubscriberRecord>> GetAllSubscribersAsync()
    {
        return await _apiClient.GetAllAsync();
    }

    public async Task<SubscriberRecord?> CreateSubscriberAsync(SubscriberRecord subscriber)
    {
        return await _apiClient.CreateAsync(subscriber);
    }

    public async Task<bool> DeleteSubscriberAsync(int id)
    {
        return await _apiClient.DeleteAsync(id);
    }

    public async Task<string?> ExportSubscribersXmlAsync()
    {
        return await _apiClient.ExportXmlAsync();
    }

    public async Task<int?> ImportSubscribersXmlAsync(string xml)
    {
        return await _apiClient.ImportXmlAsync(xml);
    }

    public async Task<ExchangeRates?> GetExchangeRatesAsync()
    {
        return await _currencyClient.GetRatesFromSekAsync();
    }

    public async Task<Ad> CreateSubscriberAdAsync(string subscriptionNumber, string name, string phone,
        string address, string postalCode, string city,
        string title, string content, decimal itemPrice)
    {
        // Validate that the subscription number exists in the API
        var exists = await _apiClient.GetBySubscriptionNumberAsync(subscriptionNumber);
        if (exists == null)
            throw new InvalidOperationException($"Subscriber '{subscriptionNumber}' not found in subscription system.");

        // Find existing advertiser or create new, using user-submitted values
        var advertiser = await _advertiserRepository.GetBySubscriptionNumberAsync(subscriptionNumber);
        if (advertiser == null)
        {
            advertiser = new Advertiser
            {
                Type = "subscriber",
                SubscriptionNumber = subscriptionNumber,
                Name = name,
                Phone = phone,
                Address = address,
                PostalCode = postalCode,
                City = city
            };
            advertiser = await _advertiserRepository.CreateAsync(advertiser);
        }
        else
        {
            // Update snapshot with user-submitted values (may include corrections)
            advertiser.Name = name;
            advertiser.Phone = phone;
            advertiser.Address = address;
            advertiser.PostalCode = postalCode;
            advertiser.City = city;
            advertiser = await _advertiserRepository.UpdateAsync(advertiser);
        }

        var ad = new Ad
        {
            AdvertiserId = advertiser.Id,
            Title = title,
            Content = content,
            ItemPrice = itemPrice,
            AdPrice = 0m // Subscribers advertise for free
        };

        return await _adRepository.CreateAsync(ad);
    }

    public async Task<Ad> CreateCompanyAdAsync(string organisationNumber, string name, string phone,
        string address, string postalCode, string city,
        string invoiceAddress, string invoicePostalCode, string invoiceCity,
        string title, string content, decimal itemPrice)
    {
        var advertiser = await _advertiserRepository.GetByOrganisationNumberAsync(organisationNumber);
        if (advertiser == null)
        {
            advertiser = new Advertiser
            {
                Type = "company",
                OrganisationNumber = organisationNumber,
                Name = name,
                Phone = phone,
                Address = address,
                PostalCode = postalCode,
                City = city,
                InvoiceAddress = invoiceAddress,
                InvoicePostalCode = invoicePostalCode,
                InvoiceCity = invoiceCity
            };
            advertiser = await _advertiserRepository.CreateAsync(advertiser);
        }

        var ad = new Ad
        {
            AdvertiserId = advertiser.Id,
            Title = title,
            Content = content,
            ItemPrice = itemPrice,
            AdPrice = 40m // Companies pay 40 SEK
        };

        return await _adRepository.CreateAsync(ad);
    }

}
