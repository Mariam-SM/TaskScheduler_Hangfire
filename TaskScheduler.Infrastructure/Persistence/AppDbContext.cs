using Microsoft.EntityFrameworkCore;
using TaskScheduler.Application.Interfaces;
using TaskScheduler.Domain.Entities;

namespace TaskScheduler.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
                    .Property(o => o.TotalPrice)
                    .HasPrecision(18, 2);


        modelBuilder.Entity<Order>().HasData(
        new Order
        {
            Id = 1,
            CustomerName = "Ahmed Ali",
            TotalPrice = 500,
            CreatedAt = new DateTime(2024, 1, 1), 
            Status = "Completed"
        },
        new Order
        {
            Id = 2,
            CustomerName = "Mariam Sayed",
            TotalPrice = 300,
            CreatedAt = new DateTime(2024, 3, 1),
            Status = "Pending"
        }

        );
    }
}
