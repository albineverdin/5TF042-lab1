using System.Xml.Serialization;
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

    public async Task<string> ExportToXmlAsync()
    {
        var subscribers = await _repository.GetAllAsync();

        var serializer = new XmlSerializer(typeof(List<Subscriber>));
        using var writer = new StringWriter();
        serializer.Serialize(writer, subscribers.ToList());
        return writer.ToString();
    }

    public async Task<int> ImportFromXmlAsync(string xml)
    {
        var serializer = new XmlSerializer(typeof(List<Subscriber>));
        using var reader = new StringReader(xml);
        var imported = (List<Subscriber>?)serializer.Deserialize(reader) ?? [];

        int inserted = 0;
        foreach (var subscriber in imported)
        {
            var existing = await _repository.GetBySubscriptionNumberAsync(subscriber.SubscriptionNumber);
            if (existing != null) continue;

            subscriber.Id = 0; // Let the database assign a new ID
            await _repository.CreateAsync(subscriber);
            inserted++;
        }

        return inserted;
    }
}
