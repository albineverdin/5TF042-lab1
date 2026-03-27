using AdSystem.Models;

namespace AdSystem.DAL;

public interface IAdvertiserRepository
{
    Task<Advertiser?> GetBySubscriptionNumberAsync(string subscriptionNumber);
    Task<Advertiser?> GetByOrganisationNumberAsync(string organisationNumber);
    Task<Advertiser> CreateAsync(Advertiser advertiser);
    Task<Advertiser> UpdateAsync(Advertiser advertiser);
}
