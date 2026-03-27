using SubscriberSystem.DAL;
using SubscriberSystem.Models;

namespace SubscriberSystem.Services;

public class SubscriberService : ISubscriberService
{
    private readonly ISubscriberRepository _repository;

    public SubscriberService(ISubscriberRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Subscriber>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<SubscriberDto?> GetBySubscriptionNumberAsync(string subscriptionNumber)
    {
        var subscriber = await _repository.GetBySubscriptionNumberAsync(subscriptionNumber);
        if (subscriber == null) return null;

        return new SubscriberDto
        {
            FirstName = subscriber.FirstName,
            LastName = subscriber.LastName,
            PhoneNumber = subscriber.PhoneNumber,
            Address = subscriber.Address,
            PostalCode = subscriber.PostalCode,
            City = subscriber.City
        };
    }

    public async Task<Subscriber> CreateAsync(Subscriber subscriber)
    {
        return await _repository.CreateAsync(subscriber);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
