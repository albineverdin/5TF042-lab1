using System.ComponentModel.DataAnnotations;

namespace AdSystem.Models.ViewModels;

public class CompanyAdViewModel
{
    [Required]
    public string OrganisationNumber { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string InvoiceAddress { get; set; } = string.Empty;

    [Required]
    public string InvoicePostalCode { get; set; } = string.Empty;

    [Required]
    public string InvoiceCity { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal ItemPrice { get; set; }
}
