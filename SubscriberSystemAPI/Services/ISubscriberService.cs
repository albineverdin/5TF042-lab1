using SubscriberSystem.Models;

namespace SubscriberSystem.Services;

public interface ISubscriberService
{
    Task<IEnumerable<Subscriber>> GetAllAsync();
    Task<SubscriberDto?> GetBySubscriptionNumberAsync(string subscriptionNumber);
    Task<Subscriber> CreateAsync(Subscriber subscriber);
    Task<bool> DeleteAsync(int id);
}
