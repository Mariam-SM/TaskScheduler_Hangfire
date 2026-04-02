using Microsoft.EntityFrameworkCore;
using TaskScheduler.Domain.Entities;

namespace TaskScheduler.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                CustomerName = "Ahmed Ali",
                TotalPrice = 500,
                CreatedAt = DateTime.UtcNow.AddDays(-60), // old -> 60 days ago 
                Status = "Completed"
            },
            new Order
            {
                Id = 2,
                CustomerName = "Mariam Sayed",
                TotalPrice = 300,
                CreatedAt = DateTime.UtcNow.AddDays(-5), // new
                Status = "Pending"
            }
        );
    }
}
