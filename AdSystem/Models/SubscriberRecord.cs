namespace AdSystem.Models;

// Matches the full Subscriber model returned by the SubscriberSystemAPI
public class SubscriberRecord
{
    public int Id { get; set; }
    public string SubscriptionNumber { get; set; } = string.Empty;
    public string NationalIdNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
