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
        // بنضيف بعض الداتا للتيست
        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                CustomerName = "Ahmed Ali",
                TotalPrice = 500,
                CreatedAt = DateTime.UtcNow.AddDays(-60), // قديم 60 يوم
                Status = "Completed"
            },
            new Order
            {
                Id = 2,
                CustomerName = "Sara Mohamed",
                TotalPrice = 300,
                CreatedAt = DateTime.UtcNow.AddDays(-5), // جديد
                Status = "Pending"
            }
        );
    }
}
