using Microsoft.EntityFrameworkCore;
using AdSystem.Data;
using AdSystem.Models;

namespace AdSystem.DAL;

public class AdRepository : IAdRepository
{
    private readonly AppDbContext _context;

    public AdRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ad>> GetAllAsync()
    {
        return await _context.Ads
            .Include(a => a.Advertiser)
            .ToListAsync();
    }

    public async Task<Ad> CreateAsync(Ad ad)
    {
        _context.Ads.Add(ad);
        await _context.SaveChangesAsync();
        return ad;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ad = await _context.Ads.FindAsync(id);
        if (ad == null) return false;
        _context.Ads.Remove(ad);
        await _context.SaveChangesAsync();
        return true;
    }
}
