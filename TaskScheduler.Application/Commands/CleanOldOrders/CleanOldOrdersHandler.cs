using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskScheduler.Application.Commands.CleanOldOrders;
using TaskScheduler.Application.Interfaces;

namespace TaskScheduler.Infrastructure;

public class CleanOldOrdersHandler 
    : IRequestHandler<CleanOldOrdersCommand, int>
{
    private readonly IAppDbContext _context;

    public CleanOldOrdersHandler(IAppDbContext context)
        => _context = context;

    public async Task<int> Handle(
        CleanOldOrdersCommand request,
        CancellationToken cancellationToken)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-request.DaysOld);

        var oldOrders = await _context.Orders
            .Where(o => o.CreatedAt < cutoffDate)
            .ToListAsync(cancellationToken);

        if (!oldOrders.Any())
            return 0;

        _context.Orders.RemoveRange(oldOrders);
        await _context.SaveChangesAsync(cancellationToken);

        return oldOrders.Count;
    }
}
