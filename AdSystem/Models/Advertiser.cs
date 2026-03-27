using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdSystem.Models;

[Table("tbl_advertisers")]
public class Advertiser
{
    [Key]
    [Column("advertiser_id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("advertiser_type")]
    public string Type { get; set; } = string.Empty; // "subscriber" or "company"

    [Required]
    [MaxLength(200)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Column("phone")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("address")]
    public string Address { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    [Column("postal_code")]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("city")]
    public string City { get; set; } = string.Empty;

    // Subscribers only
    [MaxLength(50)]
    [Column("subscription_number")]
    public string? SubscriptionNumber { get; set; }

    // Companies only
    [MaxLength(20)]
    [Column("organisation_number")]
    public string? OrganisationNumber { get; set; }

    [MaxLength(200)]
    [Column("invoice_address")]
    public string? InvoiceAddress { get; set; }

    [MaxLength(10)]
    [Column("invoice_postal_code")]
    public string? InvoicePostalCode { get; set; }

    [MaxLength(100)]
    [Column("invoice_city")]
    public string? InvoiceCity { get; set; }

    public ICollection<Ad> Ads { get; set; } = new List<Ad>();
}
