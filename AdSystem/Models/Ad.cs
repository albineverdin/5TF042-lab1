using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdSystem.Models;

[Table("tbl_ads")]
public class Ad
{
    [Key]
    [Column("ad_id")]
    public int Id { get; set; }

    [Column("advertiser_id")]
    public int AdvertiserId { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("ad_title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("ad_content")]
    public string Content { get; set; } = string.Empty;

    [Column("ad_itemprice")]
    public decimal ItemPrice { get; set; }

    [Column("ad_adprice")]
    public decimal AdPrice { get; set; }

    [ForeignKey(nameof(AdvertiserId))]
    public Advertiser Advertiser { get; set; } = null!;
}
