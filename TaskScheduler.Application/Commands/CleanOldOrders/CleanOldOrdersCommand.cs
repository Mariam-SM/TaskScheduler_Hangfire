using MediatR;

namespace TaskScheduler.Application.Commands.CleanOldOrders;

public class CleanOldOrdersCommand : IRequest<int>
{
   
    public int DaysOld { get; set; } = 30;
}
