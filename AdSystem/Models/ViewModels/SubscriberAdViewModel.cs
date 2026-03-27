using System.ComponentModel.DataAnnotations;

namespace AdSystem.Models.ViewModels;

public class SubscriberAdViewModel
{
    [Required]
    public string SubscriptionNumber { get; set; } = string.Empty;

    // Pre-filled from API, shown in form, not editable by user (but sent back on submit)
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal ItemPrice { get; set; }
}
