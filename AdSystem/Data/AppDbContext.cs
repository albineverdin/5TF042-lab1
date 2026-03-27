using Microsoft.EntityFrameworkCore;
using AdSystem.Models;

namespace AdSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Advertiser> Advertisers { get; set; }
    public DbSet<Ad> Ads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advertiser>()
            .HasIndex(a => a.SubscriptionNumber)
            .IsUnique()
            .HasFilter("subscription_number IS NOT NULL");

        modelBuilder.Entity<Advertiser>()
            .HasIndex(a => a.OrganisationNumber)
            .IsUnique()
            .HasFilter("organisation_number IS NOT NULL");
    }
}
