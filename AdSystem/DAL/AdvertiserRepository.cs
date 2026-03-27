using Microsoft.EntityFrameworkCore;
using AdSystem.Data;
using AdSystem.Models;

namespace AdSystem.DAL;

public class AdvertiserRepository : IAdvertiserRepository
{
    private readonly AppDbContext _context;

    public AdvertiserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Advertiser?> GetBySubscriptionNumberAsync(string subscriptionNumber)
    {
        return await _context.Advertisers
            .FirstOrDefaultAsync(a => a.SubscriptionNumber == subscriptionNumber);
    }

    public async Task<Advertiser?> GetByOrganisationNumberAsync(string organisationNumber)
    {
        return await _context.Advertisers
            .FirstOrDefaultAsync(a => a.OrganisationNumber == organisationNumber);
    }

    public async Task<Advertiser> CreateAsync(Advertiser advertiser)
    {
        _context.Advertisers.Add(advertiser);
        await _context.SaveChangesAsync();
        return advertiser;
    }

    public async Task<Advertiser> UpdateAsync(Advertiser advertiser)
    {
        _context.Advertisers.Update(advertiser);
        await _context.SaveChangesAsync();
        return advertiser;
    }
}
