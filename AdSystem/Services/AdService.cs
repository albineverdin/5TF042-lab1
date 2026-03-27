using AdSystem.DAL;
using AdSystem.Models;

namespace AdSystem.Services;

public class AdService : IAdService
{
    private readonly IAdvertiserRepository _advertiserRepository;
    private readonly IAdRepository _adRepository;
    private readonly SubscriberApiClient _apiClient;

    public AdService(IAdvertiserRepository advertiserRepository, IAdRepository adRepository, SubscriberApiClient apiClient)
    {
        _advertiserRepository = advertiserRepository;
        _adRepository = adRepository;
        _apiClient = apiClient;
    }

    public async Task<SubscriberResponse?> LookupSubscriberAsync(string subscriptionNumber)
    {
        return await _apiClient.GetBySubscriptionNumberAsync(subscriptionNumber);
    }

    public async Task<IEnumerable<Ad>> GetAllAdsAsync()
    {
        return await _adRepository.GetAllAsync();
    }

    public async Task<Ad> CreateSubscriberAdAsync(string subscriptionNumber, string title, string content, decimal itemPrice)
    {
        // Re-fetch from API to keep snapshot fresh
        var subscriberData = await _apiClient.GetBySubscriptionNumberAsync(subscriptionNumber);
        if (subscriberData == null)
            throw new InvalidOperationException($"Subscriber '{subscriptionNumber}' not found in subscription system.");

        // Find existing advertiser or create new
        var advertiser = await _advertiserRepository.GetBySubscriptionNumberAsync(subscriptionNumber);
        if (advertiser == null)
        {
            advertiser = new Advertiser { Type = "subscriber", SubscriptionNumber = subscriptionNumber };
            ApplySubscriberSnapshot(advertiser, subscriberData);
            advertiser = await _advertiserRepository.CreateAsync(advertiser);
        }
        else
        {
            // Update snapshot with latest data from API
            ApplySubscriberSnapshot(advertiser, subscriberData);
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

    private static void ApplySubscriberSnapshot(Advertiser advertiser, SubscriberResponse data)
    {
        advertiser.Name = $"{data.FirstName} {data.LastName}";
        advertiser.Phone = data.PhoneNumber;
        advertiser.Address = data.Address;
        advertiser.PostalCode = data.PostalCode;
        advertiser.City = data.City;
    }
}
