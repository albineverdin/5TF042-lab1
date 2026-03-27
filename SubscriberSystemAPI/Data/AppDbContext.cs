using Microsoft.EntityFrameworkCore;
using SubscriberSystem.Models;

namespace SubscriberSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Subscriber> Subscribers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subscriber>()
            .HasIndex(s => s.SubscriptionNumber)
            .IsUnique();

        modelBuilder.Entity<Subscriber>()
            .HasIndex(s => s.NationalIdNumber)
            .IsUnique();
    }
}
