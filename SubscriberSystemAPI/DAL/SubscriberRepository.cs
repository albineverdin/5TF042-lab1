using Microsoft.EntityFrameworkCore;
using SubscriberSystem.Data;
using SubscriberSystem.Models;

namespace SubscriberSystem.DAL;

public class SubscriberRepository : ISubscriberRepository
{
    private readonly AppDbContext _context;

    public SubscriberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Subscriber>> GetAllAsync()
    {
        return await _context.Subscribers.ToListAsync();
    }

    public async Task<Subscriber?> GetBySubscriptionNumberAsync(string subscriptionNumber)
    {
        return await _context.Subscribers
            .FirstOrDefaultAsync(s => s.SubscriptionNumber == subscriptionNumber);
    }

    public async Task<Subscriber> CreateAsync(Subscriber subscriber)
    {
        _context.Subscribers.Add(subscriber);
        await _context.SaveChangesAsync();
        return subscriber;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var subscriber = await _context.Subscribers.FindAsync(id);
        if (subscriber == null) return false;

        _context.Subscribers.Remove(subscriber);
        await _context.SaveChangesAsync();
        return true;
    }
}
