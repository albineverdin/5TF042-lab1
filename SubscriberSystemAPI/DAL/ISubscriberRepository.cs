using SubscriberSystem.Models;

namespace SubscriberSystem.DAL;

public interface ISubscriberRepository
{
    Task<IEnumerable<Subscriber>> GetAllAsync();
    Task<Subscriber?> GetBySubscriptionNumberAsync(string subscriptionNumber);
    Task<Subscriber> CreateAsync(Subscriber subscriber);
    Task<bool> DeleteAsync(int id);
}
